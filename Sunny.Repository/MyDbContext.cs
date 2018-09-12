using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sunny.Common.DependencyInjection;
using Sunny.Repository.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
 
namespace Sunny.Repository
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

            Database.EnsureCreated();
            //Database.Migrate();
             
        }

         
 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // IEntityTypeConfiguration
            base.OnModelCreating(modelBuilder);

            //查找所有FluentAPI配置
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);

            //应用FluentAPI
            foreach (var type in typesToRegister)
            {
                //dynamic使C#具有弱语言的特性，在编译时不对类型进行检查

                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            modelBuilder.Entity<Student>().HasOne(x => x.Address).WithOne(x=>x.Student).HasForeignKey<StudentAddress>(x=>x.Zipcode);
            //modelBuilder.Entity<Student>().HasIndex(x => x.StudentName).ForMySqlIsFullText();
        }


        public DbSet<Category> Category { get; set; }
        public DbSet<Passage> Passage { get; set; }
        public DbSet<PassageCategory> PassageCategory { get; set; }


        //public DbSet<User> Uesrs { get; set; }
        //public DbSet<UserB> Uesr2s { get; set; }
        public DbSet<Student> Student { get; set; }
       
        public DbSet<StudentAddress> StudentAddress { get; set; }
    }
}
