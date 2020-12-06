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
using Projects.PaymentServices.Context;
using Projects.PaymentServices.Repositories;
using Projects.PaymentServices.Services;

namespace Projects.PaymentServices
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
            // 1Ìí¼Ódbcontext
            services.AddDbContext<PaymentContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
            });

            // 2¡¢×¢²áÖ§¸¶service
            services.AddScoped<IPaymentService, PaymentServiceImpl>();

            // 3¡¢×¢²áÖ§¸¶²Ö´¢
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            //4.Ìí¼Ó·þÎñ×¢²á
            services.AddServiceRegistry(options =>
            {
                options.ServiceId = Guid.NewGuid().ToString();
                options.ServiceName = "PaymentServices";
                options.ServiceAddress = "http://localhost:5035";
                options.RegistryAddress = "http://localhost:8500";
                options.HealthCheckAddress = "/HealthCheck";
            });


            services.AddControllers(options=>
            {
                options.Filters.Add<MiddlewareResultWapper>(1);
                options.Filters.Add<BizExceptionHandler>(2);
            }).AddNewtonsoftJson(options=>
            {
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
