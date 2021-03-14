using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
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
using Projects.Common.Exceptions;
using Projects.Common.Exceptions.Handlers;
using Projects.Common.Filters;
using Projects.Cores.Register.Extensions;
using Projects.UserServices.Configs;
using Projects.UserServices.Context;
using Projects.UserServices.IdentityServer;
using Projects.UserServices.Reposotories;
using Projects.UserServices.Services;

namespace Projects.UserServices
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
            //1.IOC中注入dbcontext
            services.AddDbContext<UserContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
            });

            //2.注册用户service
            services.AddScoped<IUserService, UserServiceImpl>();

            //3.注册用户repository
            services.AddScoped<IUserRepository, UserRepository>();

            //4.添加服务注册
            services.AddServiceRegistry(options =>
            {
                options.ServiceId = Guid.NewGuid().ToString();
                options.ServiceName = "UserServices";
                options.ServiceAddress = "https://localhost:5005";
                options.HealthCheckAddress = "/HealthCheck";
                options.RegistryAddress = "http://localhost:8500";
            });
             //var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            //5.添加ids4
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()//1.配置签署证书
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                      {
                          builder.UseMySQL(Configuration.GetConnectionString("DefaultConnection")/*, options =>
                           options.MigrationsAssembly(migrationsAssembly)*/);
                      };
                })
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();


            //6.添加控制器
            services.AddControllers(options=>
            {
                options.Filters.Add<MiddlewareResultWapper>(1);
                options.Filters.Add<BizExceptionHandler>(2);
            }).AddNewtonsoftJson(options=>
            {
                //防止将大学转换成小写
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
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

            //使用ids4
            app.UseIdentityServer();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InitializeDatabase(app);
        }

        /// <summary>
        /// config中数据存储
        /// </summary>
        /// <param name="app"></param>
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using(var serviceScope=app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();
                context.Database.Migrate();
                //if(!context.Clients.Any())
                //{
                    context.Clients.RemoveRange(context.Clients);
                    foreach(var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                //}

                //if(!context.ApiResources.Any())
                //{
                context.ApiResources.RemoveRange(context.ApiResources);
                    foreach(var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                //}

                //if(!context.IdentityResources.Any())
                //{
                context.RemoveRange(context.IdentityResources);
                    foreach(var resource in Config.Ids)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                //}
            }
        }
    }
}
