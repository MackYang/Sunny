using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Quartz;
using Quartz.Impl;
using RepositoryDemo;
using Sunny.Api.Midware;
using Sunny.Common.ConfigOption;
using Sunny.Common.Helper;
using Sunny.Common.JsonTypeConverter;
using Sunny.Repository;
using System.IO;

namespace ApiDemo
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
            DiHelper.AutoRegister(services);

            var connection = Configuration.GetConnectionString("MySql");
            services.AddDbContext<MyDbContext>(options =>
                options.UseMySql(connection));

            ////同时使用多个数据库的DEMO
            //var connection2 = Configuration.GetConnectionString("MySq2");
            //services.AddDbContext<TDbContext>(options =>
            //   options.UseMySql(connection2));

            //services.Configure<NetLoggerOption>(Configuration.GetSection("ConfigOptions:NetLoggerOption"));

            //根据需要在这里配置要使用的Option,然后在要使用的地方通过构造器注入IOptions<TOption>得到TOption
            services.Configure<TokenValidateOption>(Configuration.GetSection("SunnyOptions:TokenValidateOption"));

            services.AddMvcCore()
                .AddFluentValidation()
                .AddJsonFormatters(x =>
                {
                    x.Converters.Add(new LongConverter());
                    x.Converters.Add(new DecimalConverter());
                    x.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    x.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddCors()
                .AddFormatterMappings()
                .AddCacheTagHelper()
                .AddDataAnnotations();

            services.AddAutoMapper();
            services.AddDistributedRedisCache(options =>
            {
                var configOption = Configuration.GetSection("SunnyOptions:RedisOptions").Get<RedisOption>();
                options.Configuration = configOption.ConnectionString;
                options.InstanceName = configOption.InstanceName;
                IDistributedCacheExtend.DefaultSlidingExpiration = configOption.DefaultSlidingExpiration;
            });
            services.AddSession();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.InitServiceProvider();
            loggerFactory.AddConsole();
            loggerFactory.AddNetLoggerUseDefaultFilter(Configuration.GetSection("SunnyOptions:NetLoggerOption").Get<NetLoggerOption>());
            IdHelper.InitSnowflake(Configuration.GetSection("SunnyOptions:SnowflakeOption").Get<SnowflakeOption>());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ErrorHandlingMiddleware>();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseMiddleware<TokenValidateMiddleware>();
            app.UseMvc();

        }
    }
}
