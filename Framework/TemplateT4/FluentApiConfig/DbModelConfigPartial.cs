using Sunny.Repository.DbModel;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sunny.TemplateT4.FluentApiConfig
{
    public partial class DbModelConfig
    {

        public Type DbModelType { get; set; }

        public string ConfigNamespace { get; set; }

        private string InheritBaseModel()
        {
            var baseFields = typeof(BaseModel).GetProperties();

            //当前类型的所有公共属性
            var allFields = DbModelType.GetProperties();

            //排除BaseModel的属性
            var selfFields = allFields.Where(x => !baseFields.Any(dx => { return dx.Name == x.Name; }));

            //排除导航属性字段,这些字段是以关系的形式处理
            var simpleTypeFields = selfFields.Where(x => !x.PropertyType.IsGenericType && !typeof(IDbModel).IsAssignableFrom(x.PropertyType));


            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.AppendLine(GetFieldConfig(baseFields.Where(x => x.Name == "Id").First()));
            simpleTypeFields.ToList().ForEach(x => sb.AppendLine(GetFieldConfig(x)));
            //排除在子类中被private的字段
            baseFields.Where(x => x.Name != "Id" && allFields.Any(n => { return n.Name == x.Name; })).ToList().ForEach(x => sb.AppendLine(GetFieldConfig(x)));

            return sb.ToString();
        }

        private string OtherModel()
        {
            //当前类型的所有公共属性
            var allFields = DbModelType.GetProperties();
             
            //排除导航属性字段,这些字段是以关系的形式处理
            var simpleTypeFields = allFields.Where(x => !x.PropertyType.IsGenericType && !typeof(IDbModel).IsAssignableFrom(x.PropertyType));

            StringBuilder sb = new StringBuilder();
            simpleTypeFields.ToList().ForEach(x => sb.AppendLine(GetFieldConfig(x)));

            return sb.ToString();
        }

        public string GetFieldsConfig()
        {
            if (typeof(BaseModel).IsAssignableFrom(DbModelType))
            {
                return InheritBaseModel();
            }
            else
            {
                return OtherModel();
            }
        }


        private string GetFieldConfig(PropertyInfo pi)
        {

            string originName = pi.Name;

            string destName = originName.UpperCharToUnderLine();

            string fieldConfig = $"builder.Property(x => x.{originName}).HasColumnName(\"{ destName}\")";

            if (originName == "CreateTime")
            {
                fieldConfig += ".HasDefaultValueSql(\"now()\")";
            }

            if (originName == "UpdateTime")
            {
                fieldConfig += ".IsRowVersion()";
            }

            if (pi.PropertyType == typeof(String))
            {
                fieldConfig += ".HasMaxLength(30)";
            }
            if (pi.PropertyType == typeof(Decimal))
            {
                fieldConfig += ".HasColumnType(\"decimal(18, 2)\")";
            }

            return fieldConfig + ";";

        }


        public DbModelConfig(Type dbModelType, string configNamespace)
        {
            this.DbModelType = dbModelType;
            this.ConfigNamespace = configNamespace;
        }
    }
}
