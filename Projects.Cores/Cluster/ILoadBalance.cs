using Projects.Cores.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.Cluster
{
    public interface ILoadBalance
    {
        /// <summary>
        /// 服务选择
        /// </summary>
        /// <param name="serviceNodes"></param>
        /// <returns></returns>
        ServiceNode Select(IList<ServiceNode> serviceNodes);
    }
}
