using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Projects.Common.Caches;
using Projects.Common.Distributes;
using Projects.Common.Exceptions.Handlers;
using Projects.Common.Filters;
using Projects.Common.Users;
using Projects.Cores.MicroClient.Extensions;
using SeckillAggregateServices.Caches.SeckillStock;
using SeckillAggregateServices.Context;
using SeckillAggregateServices.Luas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //1.注册微服务客户端（动态中台—》服务发现—》负载均衡—》中台—》PollyHttpClient）
            services.AddMicroClient(options =>
            {
                options.AssemblyName = "SeckillAggregateServices";
                options.dynamicMiddlewareOptions = mo =>
                  {
                      mo.serviceDiscoveryOptions = sdo =>
                        {
                            sdo.DiscoveryAddress = "http://localhost:8500";
                        };
                  };
            });

            //2.设置跨域
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.SetIsOriginAllowed(_=>true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });

            //3.添加身份认证
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:5005";//1.授权中心地址
                    options.ApiName = "Services";//2.Api名称
                    options.RequireHttpsMetadata = false;//3.https元数据不需要
                });

            //4.添加控制器
            services.AddControllers(options =>
            {
                options.Filters.Add<FrontResultWapper>();//1.通用结果
                options.Filters.Add<BizExceptionHandler>();//2.通用异常
                options.ModelBinderProviders.Insert(0, new SysUserModelBinderProvider());//3.自定义绑定模型
            }).AddNewtonsoftJson(options =>
            {
                //防止大写转换成小写
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            //5.使用内存缓存
            services.AddMemoryCache();

            //5.1.使用redis分布式缓存
            services.AddDistributedRedisCache("localhost:6379, password =, defaultDatabase = 2, poolsize = 50, connectTimeout = 5000, syncTimeout = 10000, prefix = seckill_stock_");

            //6 使用秒杀库存缓存
            //services.AddSeckillStockCache();
            //6.1 使用秒杀redis库存缓存
            services.AddRedisSeckillStockCache();

            //7.添加事件总线
            services.AddCap(x =>
            {
                //7.1使用内存存储消息（消息发送失败处理）
                x.UseInMemoryStorage();
                //7.2使用EntityFramework进行存储操作
                //x.UseEntityFramework<SeckillAggregateServicesContext>();
                //7.3使用mysql进行事务处理
                //x.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
                //7.4使用rabbitmq进行事件中心处理
                x.UseRabbitMQ(rb =>
                {
                    rb.HostName = "localhost";
                    rb.UserName = "guest";
                    rb.Password = "guest";
                    rb.Port = 5672;
                    rb.VirtualHost = "/";
                });
                //7.5使用后台监控页面
                x.UseDashboard();
            });

            //8.添加dbcontext
            services.AddDbContextPool<SeckillAggregateServicesContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
            });

            //9.加载seckillLua文件
            services.AddHostedService<SeckillLuaHostedService>();

            //10添加分布式订单
            services.AddDistributedOrderSn(1, 1);


            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //2.使用跨域
            app.UseCors("AllowSpecificOrigin");

            app.UseHttpsRedirection();

            app.UseRouting();

            //1.开启身份认证
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
