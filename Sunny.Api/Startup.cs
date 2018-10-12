using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sunny.Api.Midware;
using Sunny.Common.ConfigOption;
using Sunny.Common.DependencyInjection;
using Sunny.Repository;
using System.IO;

namespace Sunny.Api
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

         

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            var connection = Configuration.GetConnectionString("MySql");
            services.AddDbContext<EfDbContext>(options =>
                options.UseMySql(connection));

            ////同时使用多个数据库的DEMO
            //var connection2 = Configuration.GetConnectionString("MySq2");
            //services.AddDbContext<TDbContext>(options =>
            //   options.UseMySql(connection2));

            //services.Configure<NetLoggerOption>(Configuration.GetSection("ConfigOptions:NetLoggerOption"));


            DIHelper.AutoRegister(services);
       
            services.AddMvc();
            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddNetLoggerUseDefaultFilter(Configuration.GetSection("ConfigOptions:NetLoggerOption").Get<NetLoggerOption>());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ErrorHandlingMiddleware>(loggerFactory);
            }
            app.UseMvc();
            app.UseStaticFiles();
            
        }
    }
}
