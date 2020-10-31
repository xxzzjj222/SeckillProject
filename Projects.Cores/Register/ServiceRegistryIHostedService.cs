using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projects.Cores.Register
{
    /// <summary>
    /// 服务启动时自动注册
    /// </summary>
    public class ServiceRegistryIHostedService : IHostedService
    {
        private readonly IServiceRegistry _serviceRegistry;

        public ServiceRegistryIHostedService(IServiceRegistry serviceRegistry)
        {
            _serviceRegistry = serviceRegistry;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            /* // 1、从IOC容器中获取Consul服务注册配置
            var serviceNode = app.ApplicationServices.GetRequiredService<IOptions<ServiceRegistryConfig>>().Value;

            // 2、获取应用程序生命周期
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            // 2.1 获取服务注册实例
            var serviceRegistry = app.ApplicationServices.GetRequiredService<IServiceRegistry>();*/

            // 3、获取服务地址
            /*var features = app.Properties["server.Features"] as FeatureCollection;
            var address = features.Get<IServerAddressesFeature>().Addresses.First();*/
            //var uri = new Uri(address);

            // 4、注册服务
            /* serviceNode.Id = Guid.NewGuid().ToString();
             //serviceNode.Address = $"{uri.Scheme}://{uri.Host}";
             serviceNode.Address = uri.Host;
             serviceNode.Port = uri.Port;
             serviceNode.HealthCheckAddress = $"{uri.Scheme}://{uri.Host}:{uri.Port}{serviceNode.HealthCheckAddress}";*/

            /*// 5、服务器关闭时注销服务
            lifetime.ApplicationStopping.Register(() =>
            {
                serviceRegistry.Deregister(serviceNode);
            });*/
            return Task.Run(() => _serviceRegistry.Register());
        }

        /// <summary>
        /// 服务停止时自动注销
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _serviceRegistry.Deregister();
            return Task.CompletedTask;
        }
    }
}
