using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sunny.Api.Midware;
using Sunny.Common.ConfigOption;
using Sunny.Common.DependencyInjection;
using Sunny.Repository;

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

        ////创建日志工厂
        //private static ILoggerFactory MyLoggerProvider => new LoggerFactory()
        //         .AddDebug((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name))
        //        .AddConsole((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name));


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
            app.UseMiddleware<ErrorHandlingMiddleware>(loggerFactory);
            
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //loggerFactory.AddDebug();
            //loggerFactory.AddProvider(new MyFilteredLoggerProvider());
            //loggerFactory.AddConsole((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name));
            //loggerFactory.AddConsole((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == "Microsoft.EntityFrameworkCore.Database.Command"));

            //loggerFactory.AddConsole();
            loggerFactory.AddNetLogger(Configuration.GetSection("ConfigOptions:NetLoggerOption").Get<NetLoggerOption>());
            //loggerFactory.AddNetLogger((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name));

            //app.Use(async (ctx, next) =>
            //{
            //    IGreeter greeter = ctx.RequestServices.GetService<IGreeter>();
            //    await ctx.Response.WriteAsync(greeter.Greet());
            //    await next();
            //});

            app.UseMvc();
           
        }
    }
}
