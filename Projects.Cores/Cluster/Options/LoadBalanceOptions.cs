using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.Cluster.Options
{
    public class LoadBalanceOptions
    {
        public LoadBalanceOptions()
        {
            this.Type = "Random";
        }

        /// <summary>
        /// 负载均衡选项
        /// </summary>
        public string Type { get; set; }
    }
}
