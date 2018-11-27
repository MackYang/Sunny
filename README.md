# Sunny
**这是一个基于AspNetCore+EFCore的快速开发框架(NetCore2.1),用更少的代码去实现目标,希望可以为更多的人节省时间.**


### 目前已集成和实现的内容

- <a href="#snowflakeId">SnowflakeId (Twitter的雪花Id算法)</a>
- <a href="#autoMapper">AutoMapper (实体间类型转换)</a>
- <a href="#quartZ">Quartz (定时任务)</a>
- <a href="#apiValidation">FluentValidation Api参数模型验证</a>
- Api参数模型绑定
- Swagger Api文档生成
- T4模板用于为DbModel生成EFCore使用的FluentApi配置文件
- 全局异常处理中间件
- Token验证中间件
- 网络日志 记录失败时会记到本地文件
- 自动依赖注入
- Redis
- Long,Decimal,DateTime的Json处理
- Api统一返回格式 (code,data,msg)
- 类型扩展动态属性
- 分页处理
- 网络铺助(发邮件,发短信,Ip信息查询)
- 字符串与枚举相关的扩展
- 图片缩放,水印,验证码图片等
- Xml文件,文本文件读取写入
- 加解密相关
- Base64序列化相关

---

### 未来打算集成和实现的内容

- *Lucence*
- *RabbitMq*
- *支付相关(微信,支付宝)*
- *SignalR*
- *微信登录*
- *基于Vue的后台管理UI*
- *Sso*

---  

### 使用文档

#### 快速开始


**(备注:文档中的斜体部分是作为建议,不是必须的.)**

<br/>
<br/>

**创建一个AspNetCore的WebApi项目**,从Nuget中搜索SunnyApi并引用

![](Doc/packApi.png)


*建议将UseDemo\ApiDemo中的[Program.cs](https://github.com/MackYang/Sunny/blob/master/UseDemo/ApiDemo/Program.cs)类定义部分复制到您的Program.cs中.*

请将UseDemo\ApiDemo中的[appsettings.json](https://github.com/MackYang/Sunny/tree/master/UseDemo/ApiDemo/appsettings.json)文件复制到您的项目中进行替换,并将[StartUp.cs](https://github.com/MackYang/Sunny/tree/master/UseDemo/ApiDemo/Startup.cs)类定义部分复制到您的StartUp.cs中,在报红的地方按Alt+Enter引入对应的命名空间.


将StartUp.cs中的MyDbContext部分换成您项目中自定义的数据库上下文.

![](Doc/myDbContext.png)


如果您启用了Swagger,请设置您项目的生成选项,输出xml以便SwaggerUI中能看到Api的注释内容
![](Doc/commentXml.png)


修改下图中的xml文件为您项目的xml文件名称
![](Doc/swaggerXml.png)

<br/>
<br/>

**创建一个NetCore的类库项目**,作为您的数据库访问层,并在该项目中引入Sunny.Repository的Nuget包

![](Doc/packRepository.png)


新建一个类作为您的数据库上下文,如MyDbContext,将RepositoryDemo中的[MyDbContext.cs](https://github.com/MackYang/Sunny/blob/master/UseDemo/RepositoryDemo/MyDbContext.cs)类定义部分的代码复制到您的类中.

记得将FluentApiTools.ApplyDbModelFluentApiConfig(modelBuilder, "RepositoryDemo");中的"RepositoryDemo"修改成您Repository项目的名称,以便接下来应用FluentApi的DbModel配置.


新建一个类作为您设计时的上下文工厂,并请将[DesignTimeDbContextFactory](https://github.com/MackYang/Sunny/blob/master/UseDemo/RepositoryDemo/DesignTimeDbContextFactory.cs)中的类定义部分代码复制到该类中,
用于将DbModel的修改应用到数据库中.


新建一个DbModel文件夹,内部结构可参照RepositoryDemo项目中的[DbModel](https://github.com/MackYang/Sunny/tree/master/UseDemo/RepositoryDemo/DbModel).


*Model用于存放您定义的DbModel类*
*ModelConfig用于存放FluentApi的配置,这个文件夹中的文件是自动生成的,主要作用是将DbModel中的大写转成数据库中的下划线,配置id等,字段的内容长度等,通常生成完成后,需要手动修改某些字段长度,生成部分具体操作请见<a href="#t4DbModel">使用T4模板自动生成DbModel的FluentApi配置</a>*
*RelationMap用于存放您DbModel关系间的配置,在遵循微软EF约定的默认关系规则情况下,通常情况不需要手动配置*


*建议您的DbModel继承自BaseModel,BaseModel里包含了Id,CreaterId,CreateTime,UpdaterId,UpdateTime等.*

*如:*

``` cs
 public class Student : BaseModel
    {
        public Student() { }

        public string StudentName { get; set; }

        public int Age { get; set; }

    }

```


*建议新建一个DbSet.cs文件,作为MyDbContext的分部类,把所有的DbSet放在该文件中*

*如:*
``` cs
 public partial class MyDbContext
    {
        public DbSet<Student> Student { get; set; }

        public DbSet<StudentAddress> StudentAddress { get; set; }

        public DbSet<IdTest> IdTest { get; set; }
    }

```


<br/>
<br/>
<a name="t4DbModel">使用T4模板自动生成DbModel的FluentApi配置</a>

**创建一个NetCore的控制台应用程序项目**,用于生成T4模板,并在该项目中引用您的Repository项目,以及引用Sunny.TemplateT4的Nuget包
![](Doc/packT4.png)

将UseDemo/TemplateT4Demo下的[Program.cs](https://github.com/MackYang/Sunny/blob/master/UseDemo/TemplateT4Demo/Program.cs)类定义部分复制到您的Program.cs中替换后修改下图所示的地方为您的Repository项目名称.
![](Doc/T4Repository.png)

运行该项目,如果没有指定输出路径,默认输出到D:\SunnyFramework\Output\DbConfig\下,打开该目录,把生成的文件复制到您Repository项目中的ModelConfig文件夹下按实际需要做相应修改.


*DbModel写好之后,再用T4模板生成FluentApi配置,就可以通过Add-Migration xx 和 Update-Database将DbModel应用到数据库中了.*


*建议创建一个Service项目对Repository中的数据库层进行业务逻辑封装,再提供给Api层调用.*

---

#### <a name="snowflakeId">使用SnowflakeId</a>

在StartUp.cs文件Configure方法中进行初始化
```cs
IdHelper.InitSnowflake(Configuration.GetSection("SunnyOptions:SnowflakeOption").Get<SnowflakeOption>());

```
然后在您要使用的地方进行调用
```cs
 IdTest model = new IdTest();
 model.Id = IdHelper.GenId();
```

---

#### <a name="autoMapper">使用AutoMapper</a>

在StartUp.cs文件ConfigureServices方法中启用AutoMapper

```cs
services.AddAutoMapper();
```

创建一个类,继承自Profile类,并在构造器里配置各类型的转换关系

```cs
    public class ResponseMapperConfig : Profile
    {
        public ResponseMapperConfig()
        {

            CreateMap<IdTest, Customer>()
                .ForMember(cus => cus.LocalType, opt => opt.MapFrom(id => id.requestType)).ReverseMap();
            //手动指定字段映射关系
            //.ForMember(cus => cus.LocalType, opt => opt.MapFrom(id => id.requestType))
            //.ReverseMap()映射反向转换

            CreateMap<Buyer, Seller>().ReverseMap();
           
        }


    }
```

在要使用的类里通过构造函数注入一个IMapper类型的对象mapper,然后在需要转换的地方获取转换后的类型

``` cs

IMapper mapper;

public SomeClass(IMapper mapper)
{
    this.mapper=mapper;
}

public void SomeMethod()
{
    Buyer getBuyer = mapper.Map<Buyer>(seller);
}
```

---

#### <a name="quartZ">使用定时任务</a>

在Api项目下创建一个类,实现IJobEntity接口

```cs
 public class JobB : IJobEntity
    {

        public string JobName => "this job B Name";

        public string Describe => "this job B Describe";

        IStudentServic studentServic;

        public JobB(IStudentServic studentServic)
        {
            this.studentServic = studentServic;

        }


        public async Task ExecuteAsync(IJobExecutionContext jobContext)
        {
            Console.WriteLine(jobContext.JobDetail.JobDataMap["pxxx"]);//使用了配置中传来的参数,参数的名称要和配置里的一样
            Console.WriteLine( (await studentServic.GetStudent()).StudentName);

        }

    }
```

在appsetting.json中配置任务:

```json
"JobOption": [
      {
        //Job所的的类名称
        "JobClassName": "JobB",
        //Job所属的组,同一组中不能有2个相同的任务
        "JobGroup": "group1",
        //Job在什么时候运行,用Cron表达式
        "RunAtCron": "*/55 * * * * ?",
        //Job的参数,没有可以不写
        "Args": {
          //参数名字和你在任务中写的要相同
          "pxxx": "kkk",
          "nnn": 123
        }
      },

      {
        //Job所的的类名称
        "JobClassName": "JobA",
        //Job在什么时候运行,用Cron表达式
        "RunAtCron": "*/59 * * * * ?"

      }
    ]
```

在StartUp.cs的ConfigureServices方法中注册任务服务
```cs
services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。
```

在StartUp.cs的Configure方法中启用Job,启用后,Job会在配置的时间运行

```cs
 public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ISchedulerFactory schedulerFactory)
        {
            app.InitServiceProvider();
            app.EnableJob(Configuration, schedulerFactory);
        }
```

---

#### <a name="apiValidation">Api参数验证</a>

在StartUp.cs中注册Fluent验证:
```cs
services.AddMvcCore()
                .AddFluentValidation()
```

在Api项目中创建一个类,继承自Validator<T>,如:

```cs
 public class CustomerValidator : Validator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Forename).NotEmpty().WithMessage("PleasFFe specify a first name");
            RuleFor(x => x.Discount).NotEqual(0).When(x => x.HasDiscount);
            RuleFor(x => x.Address).Length(20, 250);
            RuleFor(x => x.Postcode).Must(BeAValidPostcode).WithMessage("Please specify a valid postcode");
        }

        private bool BeAValidPostcode(string postcode)
        {
            return postcode == "123";
            // custom postcode validating logic goes here
        }
    }
```

在Api中直接使用实体Customer,进入方法之前会先对customer验证,如果验证不通过不会进入方法内部,会返回相应的提示信息:
```cs
 /// <summary>
        /// 带返回值的成功场景测试,测试模型验证
        /// </summary>
        /// <returns></returns>
        [HttpPost("Get2")]
        public Result<A> Get2(Customer customer)
        {

            return this.Success(new A { FullName = "AbcYH", Age = 123.123456789m, MFF = long.MaxValue });
        }
```

---

使用文档不断完善中,如果在使用中遇到问题,可以查看UseDemo或到技术交流QQ群852498368寻求帮助.

如果该框架有帮助到您,请送上您的小星星哦.

如果您愿意贡献代码,请尽情的Fork吧,希望更多的人因为我们的存在而让生活变得更加美好!

