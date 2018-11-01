using Microsoft.EntityFrameworkCore;
using Sunny.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDemo
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            FluentApiTools.ApplyDbModelFluentApiConfig(modelBuilder);
        }
    }

   
     
}
