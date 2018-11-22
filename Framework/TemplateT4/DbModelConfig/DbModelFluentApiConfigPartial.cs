using Sunny.Repository.DbModel;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sunny.TemplateT4.DbModelConfig
{
    public partial class DbModelFluentApiConfig
    {

        public Type DbModelType { get; set; }

        public string ConfigNamespace { get; set; }

        public string GetFieldsConfig()
        {


            var baseFields = typeof(BaseModel).GetProperties();

            //取到自己的属性字段,不要BaseModel的
            var selfFields = DbModelType.GetProperties().Where(x => !baseFields.Any(dx => { return dx.Name == x.Name; }));

            //排除导航属性字段,这些字段是以关系的形式
            var simpleTypeFields = selfFields.Where(x => !x.PropertyType.IsGenericType && !typeof(BaseModel).IsAssignableFrom(x.PropertyType));


            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.AppendLine(GetFieldConfig(baseFields.Where(x=>x.Name=="Id").First()));
            simpleTypeFields.ToList().ForEach(x => sb.AppendLine(GetFieldConfig(x)));
            baseFields.Where(x=>x.Name!="Id").ToList().ForEach(x => sb.AppendLine(GetFieldConfig(x)));

            return sb.ToString();
           
        }


        static string GetFieldConfig(PropertyInfo pi)
        {

            string originName = pi.Name;

            string destName = originName.UpperCharToUnderLine();

            string fieldConfig = $"builder.Property(x => x.{originName}).HasColumnName(\"{ destName}\")";


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


        public DbModelFluentApiConfig(Type dbModelType, string configNamespace)
        {
            this.DbModelType = dbModelType;
            this.ConfigNamespace = configNamespace;
        }
    }
}
