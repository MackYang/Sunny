using Sunny.Repository.DbModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TemplateT4
{
    class Program
    {
        static void Main(string[] args)
        {

            var fx = "StudentId".UpperCharToUnderLine();
           
            Assembly assembly = Assembly.Load("Sunny.Repository");
            List<Type> ts = assembly.GetTypes().ToList();

            var baseFields = typeof(BaseModel).GetProperties();

            foreach (var item in ts.Where(s => !s.IsInterface && typeof(BaseModel).IsAssignableFrom(s) && !s.GetTypeInfo().IsAbstract&&s.FullName!= "Sunny.Repository.DbModel.BaseModel"))
            {
                //取到自己的属性字段,不要BaseModel的
                var selfFields = item.GetProperties().Where(x => !baseFields.Any(dx=> { return dx.Name == x.Name; }));

                //排除导航属性字段,这些字段是以关系的形式
                var simpleTypeFields = selfFields.Where(x=>!x.PropertyType.IsGenericType&&!typeof(BaseModel).IsAssignableFrom(x.PropertyType));


                simpleTypeFields.ToList().ForEach(x => Console.WriteLine(GetFieldConfig(x)));


            }



            baseFields.ToList().ForEach(x => Console.WriteLine(GetFieldConfig(x)));

            Console.WriteLine("Hello World!");

            Console.Read();
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

            return fieldConfig+";";

        }
    }
}
