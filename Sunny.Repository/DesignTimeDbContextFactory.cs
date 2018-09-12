using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Repository
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EfDbContext>

    { 

        public EfDbContext CreateDbContext(string[] args)

        {

            var builder = new DbContextOptionsBuilder<EfDbContext>();
            
             
            builder.UseMySql("server=localhost;database=test;user=root;password=myAdmin.;");
            
            return new EfDbContext(builder.Options);


        }

    }
}
