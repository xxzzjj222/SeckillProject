using Consul;
using Microsoft.Extensions.Options;
using Projects.Cores.Register.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.Register.Consul
{
    public class ConsulServiceRegistry : IServiceRegistry
    {
        private readonly ServiceRegistryOptions _serviceRegistryOptions;

        public ConsulServiceRegistry(IOptions<ServiceRegistryOptions> options)
        {
            _serviceRegistryOptions = options.Value;
        }

        /// <summary>
        /// 注销服务
        /// </summary>
        public void Deregister()
        {
            //1.创建consul连接
            var consulClient = new ConsulClient(config =>
            {
                config.Address = new Uri(_serviceRegistryOptions.RegistryAddress);
            });

            //2.注销服务
            consulClient.Agent.ServiceDeregister(_serviceRegistryOptions.ServiceId).Wait();

            Console.WriteLine($"服务注销成功:{ _serviceRegistryOptions.ServiceAddress}");

            //关闭连接
            consulClient.Dispose();
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        public void Register()
        {
            //1.创建consul客户端连接
            var consulClient = new ConsulClient(config =>
              {
                  config.Address = new Uri(_serviceRegistryOptions.RegistryAddress);
              });

            //2.获取服务url
            var uri = new Uri(_serviceRegistryOptions.ServiceAddress);

            //3.创建服务注册配置
            var registration = new AgentServiceRegistration()
            {
                ID = string.IsNullOrEmpty(_serviceRegistryOptions.ServiceId) ? Guid.NewGuid().ToString() : _serviceRegistryOptions.ServiceId,
                Name = _serviceRegistryOptions.ServiceName,
                Address = uri.Host,
                Port = uri.Port,
                Tags = _serviceRegistryOptions.ServiceTags,
                Check = new AgentServiceCheck
                {
                    //3.1 consul健康检查超时时间
                    Timeout = TimeSpan.FromSeconds(10),
                    //3.2 服务停止5秒后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    //3.3 consul健康检查地址
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}{_serviceRegistryOptions.HealthCheckAddress}",
                    //3.4 consul健康检查间隔时间
                    Interval = TimeSpan.FromSeconds(10)
                }
            };

            //4.注册服务
            consulClient.Agent.ServiceRegister(registration).Wait();
            Console.WriteLine($"服务注册成功:{_serviceRegistryOptions.ServiceAddress}");

            //5.关闭consul连接
            consulClient.Dispose();
        }
    }
}
