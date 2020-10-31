using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Projects.Cores.Register.Options
{
    /// <summary>
    /// 服务发现选项
    /// </summary>
    public class ServiceDiscoveryOptions
    { 
        public ServiceDiscoveryOptions()
        {
            //默认地址
            DiscoveryAddress = "http://localhost:8500";
        }
            
        public string DiscoveryAddress { get; set; }
    }

}
