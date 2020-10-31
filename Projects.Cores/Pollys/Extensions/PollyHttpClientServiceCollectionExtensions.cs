using Microsoft.Extensions.DependencyInjection;
using Polly;
using Projects.Cores.Pollys.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Cores.Pollys.Extensions
{
    /// <summary>
    /// 微服务中httpclient熔断降级策略扩展
    /// </summary>
    public static class PollyHttpClientServiceCollectionExtensions
    {
        /// <summary>
        /// httpclient扩展方法
        /// </summary>
        /// <param name="services">ioc容器</param>
        /// <param name="name">httpclient名称（针对不同的服务进行熔断降级）</param>
        /// <param name="options">熔断降级选项</param>
        /// <returns></returns>
        public static IServiceCollection AddPollyHttpClient(this IServiceCollection services,string name,Action<PollyHttpClientOptions> options)
        {
            //1.创建配置选项类
            PollyHttpClientOptions pollyHttpClientOptions = new PollyHttpClientOptions();
            options(pollyHttpClientOptions);

            //2.封装降级消息
            var fallbackResponse = new HttpResponseMessage
            {
                Content = new StringContent(pollyHttpClientOptions.ResponseMessage), //降级消息
                StatusCode = HttpStatusCode.GatewayTimeout //降级状态码
            };

            //3.配置熔断降级策略
            services.AddHttpClient(name)
                //3.1降级策略
                .AddPolicyHandler(Policy<HttpResponseMessage>.HandleInner<Exception>().FallbackAsync(fallbackResponse, async b =>
                 {
                    //1.降级打印异常
                    Console.WriteLine($"服务{name}开始降级，异常消息：{b.Exception.Message}");
                    //2.降级后的数据
                    Console.WriteLine($"服务{name}降级内容响应：{fallbackResponse.RequestMessage},{fallbackResponse.Content}");
                     await Task.CompletedTask;
                 }))
                //3.2熔断策略
                .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().CircuitBreakerAsync(pollyHttpClientOptions.CircuitBreakerOpenFallCount,
                TimeSpan.FromSeconds(pollyHttpClientOptions.CircuitBreakerDownTime), (ex, ts) =>
                 {
                     Console.WriteLine($"服务{name}断路器开启，异常消息：{ex.Exception.Message}");
                     Console.WriteLine($"服务{name}断路器开启时间：{ts.TotalSeconds}s");
                 }, () =>
                 {
                     Console.WriteLine($"服务{name}断路器关闭");
                 }, () =>
                 {
                     Console.WriteLine($"服务{name}断路器半开启(时间控制，自动开关)");
                 }))
                //3.3 重试策略
                .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().RetryAsync(pollyHttpClientOptions.RetryCount))
                //3.4 超时策略
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(pollyHttpClientOptions.TimeoutTime)));

            return services;
        }
    }
}
