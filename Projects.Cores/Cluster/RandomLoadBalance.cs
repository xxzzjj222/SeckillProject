using Projects.Cores.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.Cluster
{
    /// <summary>
    /// 加权随机算法
    /// </summary>
    public class RandomLoadBalance : AbstractLoadBalance
    {
        private readonly Random random = new Random();

        public override ServiceNode DoSelect(IList<ServiceNode> serviceNodes)
        {
            int length = serviceNodes.Count;//number of servicenodes
            int totalWeight = 0;//the sum of weights
            bool sameWeight = true;//Every serviceNode have the same weight

            for (int i = 0; i < length; i++)
            {
                int weight = GetWeight();
                totalWeight += weight;
                if (sameWeight && i>0 && weight!=GetWeight())
                {
                    sameWeight = false;
                }
            }

            if (totalWeight>0 && !sameWeight)
            {
                //if(not everyone invoker has the same weight && at least one invoker's weight>0),select readonly based on totalWeight
                int offset = random.Next(totalWeight);

                //return a invoker based on random value
                for (int i = 0; i < length; i++)
                {
                    offset -= GetWeight();
                    if (offset<0)
                    {
                        return serviceNodes[i];
                    }
                }
            }

            //if all invoker has the same weight value or totalweight=0, return evenly
            return serviceNodes[random.Next(length)];
        }
    }
}
