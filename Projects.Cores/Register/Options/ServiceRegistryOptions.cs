using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.Register.Options
{
    /// <summary>
    /// 服务注册选项
    /// </summary>
    public class ServiceRegistryOptions
    {
        public ServiceRegistryOptions()
        { }

        /// <summary>
        /// 服务ID
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务地址http://localhost:5001
        /// </summary>
        public string ServiceAddress { get; set; }

        /// <summary>
        /// 服务标签（版本）
        /// </summary>
        public string[] ServiceTags { get; set; }


        /*// 服务地址(可以选填 === 默认加载启动路径(localhost))
        public string ServiceAddress { set; get; }

        // 服务端口号(可以选填 === 默认加载启动路径端口)
        public int ServicePort { set; get; }

        // Https 或者 http
        public string ServiceScheme { get; set; }*/

        /// <summary>
        /// 服务注册地址
        /// </summary>
        public string RegistryAddress { get; set; }

        /// <summary>
        /// 健康检查地址
        /// </summary>
        public string HealthCheckAddress { get; set; }
    }
}
