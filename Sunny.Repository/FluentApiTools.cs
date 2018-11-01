using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Sunny.Repository
{

    public class FluentApiTools { 

        
        /// <summary>
        /// 对DbModel应用FlentApi字段配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ApplyDbModelFluentApiConfig(ModelBuilder modelBuilder)
        {
             
            /*下边这段代码,如果项目中使用了2个以上的DbContext,也就是使用2个数据库,那么在CodeFirst场景中,
            通过控制台命令进行数据库迁移时,会将2个数据库的表都生成到目标库里,
            有一个解决的办法是在DbModel中继续一个自定义的接口,用以筛选要迁移的表,
            然后在下边的语句中加筛选条件,如.Where(q => q.GetInterface(typeof(你定义的接口).FullName) != null&&q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);
           
            还有一个更好的办法就是为新的库新建立一个项目(程序集),不要将2个DbContext放于一个项目(程序集)中
             * 
             */

            //查找所有FluentAPI配置
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);

            //应用FluentAPI
            foreach (var type in typesToRegister)
            {
                //dynamic使C#具有弱语言的特性，在编译时不对类型进行检查

                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
                
            }
 
        }


    }
}
