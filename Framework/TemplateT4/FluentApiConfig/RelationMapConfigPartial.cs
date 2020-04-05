using Sunny.Repository.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sunny.TemplateT4.FluentApiConfig
{
    public partial class RelationMapConfig
    {
        public Type DbModelType { get; set; }

        public string ConfigNamespace { get; set; }


        public string GetRelationConfig()
        {
            //当前类型的所有公共属性
            var allFields = DbModelType.GetProperties();

            //获取简单类型字段
            var simpleTypeFields = allFields.Where(x => !x.PropertyType.IsGenericType && !typeof(IDbModel).IsAssignableFrom(x.PropertyType));

            //导航属性字段,表示关系
            var navigationFields = allFields.Where(x => x.PropertyType.IsGenericType || typeof(IDbModel).IsAssignableFrom(x.PropertyType)).ToList();

            if (simpleTypeFields.Count() != 2|| navigationFields.Count()!=2)
            {
                throw new Exception($"实体{DbModelType}的导航属性配置不正确,在多对多的关系映射中,必须包含2个Model的Id以及对应的导航属性,如果不是多对多的映射,不需要继承自IRelationMap接口,直接遵守微软约定大约配置的法则EFCore即可自动配置.");
            }

            StringBuilder sb = new StringBuilder();

            //复合主键设置
            var mutliKey = $"builder.HasKey(t => new { "{" + string.Join(',', simpleTypeFields.Select(x => "t." + x.Name)) + "}" });";
            sb.AppendLine(mutliKey);

            var leftModelName = navigationFields[0].Name;

            var leftModelNavigations = navigationFields[0].PropertyType.GetProperties().Where(x => x.PropertyType.GenericTypeArguments.Any(n => n == DbModelType));
            if (leftModelNavigations.Count() != 1)
            {
                throw new Exception($"类型{navigationFields[0].PropertyType}中有且只能有一个泛型类型为{DbModelType}的导航属性");
            }

            var leftModelNavigationFieldName = leftModelNavigations.First().Name;

            var rightModelName = navigationFields[1].Name;

            var rightModelNavigations = navigationFields[1].PropertyType.GetProperties().Where(x => x.PropertyType.GenericTypeArguments.Any(n => n == DbModelType));
            if (rightModelNavigations.Count() != 1)
            {
                throw new Exception($"类型{navigationFields[0].PropertyType}中有且只能有一个泛型类型为{DbModelType}的导航属性");
            }

            var rightModelNavigationFieldName = rightModelNavigations.First().Name;

            //配置左边的一对多关系
            var leftConfig = $"builder.HasOne(t => t.{leftModelName}).WithMany(x => x.{leftModelNavigationFieldName}).HasForeignKey(t => t.{leftModelName+"Id"});";

            //配置右边的一对多关系
            var rightConfig = $"builder.HasOne(t => t.{rightModelName}).WithMany(x => x.{rightModelNavigationFieldName}).HasForeignKey(t => t.{rightModelName + "Id"});";

            sb.AppendLine(leftConfig);
            sb.AppendLine(rightConfig);

            return sb.ToString();
        }

 

        public RelationMapConfig(Type dbModelType, string configNamespace)
        {
            this.DbModelType = dbModelType;
            this.ConfigNamespace = configNamespace;
        }
    }
}
