using Projects.Cores.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.Cluster
{
    public abstract class AbstractLoadBalance : ILoadBalance
    {
        public ServiceNode Select(IList<ServiceNode> serviceNodes)
        {
            if (serviceNodes == null || serviceNodes.Count == 0)
                return null;
            if (serviceNodes.Count == 1)
                return serviceNodes[0];
            return DoSelect(serviceNodes);
        }

        /// <summary>
        /// 子类实现
        /// </summary>
        /// <param name="serviceNodes"></param>
        /// <returns></returns>
        public abstract ServiceNode DoSelect(IList<ServiceNode> serviceNodes);

        protected int GetWeight()
        {
            int weight = 100;
            if (weight>0)
            {
                long timestamp = 0L;
                if (timestamp>0L)
                {
                    int uptime = (int)(DateTime.Now.ToFileTimeUtc() - timestamp);
                    int warmup = 10 * 60 * 1000;
                    if (uptime>0 && uptime<warmup)
                    {
                        weight = CalculateWarmupWeight(uptime, warmup, weight);
                    }
                }
            }

            return weight;
        }

        static int CalculateWarmupWeight(int uptime,int warmup,int weight)
        {
            int ww = uptime / (warmup / weight);
            return ww < 1 ? 1 : (ww > weight ? weight : ww);
        }
    }

}
