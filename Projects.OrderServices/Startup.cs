using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Projects.Common.Exceptions.Handlers;
using Projects.Common.Filters;
using Projects.Cores.Register.Extensions;
using Projects.OrderServices.Context;
using Projects.OrderServices.Repositories;
using Projects.OrderServices.Services;

namespace Projects.OrderServices
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
            //1.注入DbContext
            services.AddDbContext<OrderContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
            });

            //2添加servcies
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            //3.添加仓储层
            services.AddScoped<IOrderService, OrderServiceImpl>();
            services.AddScoped<IOrderItemService, OrderItemServiceImpl>();

            //4.添加服务注册
            services.AddServiceRegistry(options =>
            {
                options.ServiceId = Guid.NewGuid().ToString();
                options.ServiceName = "OrderServices";
                options.ServiceAddress = "http://localhost:5025";
                options.HealthCheckAddress = "/HealthCheck";
                options.RegistryAddress = "http://localhost:8500";
            });

            services.AddControllers(options=>
            {
                options.Filters.Add<MiddlewareResultWapper>(1);
                options.Filters.Add<BizExceptionHandler>(2);
            }).AddNewtonsoftJson(options=>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            //5.添加事件总线
            services.AddCap(options =>
            {
                //使用ef进行存储
                options.UseEntityFramework<OrderContext>();
                //使用mysql进行事务中心处理
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
                //使用rabbitmq进行事件中心处理
                options.UseRabbitMQ(rb =>
                {
                    rb.HostName = "localhost";
                    rb.UserName = "guest";
                    rb.Password = "guest";
                    rb.Port = 5672;
                    rb.VirtualHost = "/";
                });

                // 配置定时器尽早启动
                // x.FailedRetryInterval = 2;

                options.FailedRetryCount = 5;

                //后台管理页面
                options.UseDashboard();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
