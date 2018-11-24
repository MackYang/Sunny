# Sunny
**这是一个基于AspNetCore+EFCore的快速开发框架,用更少的代码去实现目标,希望可以为更多的人节省时间.**


### 目前已集成和实现的内容

- <a href="#snowflakeId">SnowflakeId (Twitter的雪花Id算法)</a>
- <a href="#autoMapper">AutoMapper (实体间类型转换)</a>
- Quartz (定时任务)
- FluentValidation Api参数模型验证
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

  

### 使用文档

#### 快速开始

创建一个AspNetCore的WebApi项目,从Nuget中搜索SunnyApi并引用



请将UseDemo\ApiDemo中的[StartUp.cs](https://github.com/MackYang/Sunny/tree/master/UseDemo/ApiDemo/Startup.cs)文件和[appsettings.json](https://github.com/MackYang/Sunny/tree/master/UseDemo/ApiDemo/appsettings.json)文件复制到您的项目中进行替换,并按实际需要进行修改


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

使用文档不断完善中,如果在使用中遇到问题,可以查看UseDemo或到技术交流QQ群852498368寻求帮助.

如果该框架有帮助到您,请送上您的小星星哦.

如果您愿意贡献代码,请尽情的Fork吧,希望更多的人因为我们的存在而让生活变得更加美好!

