using Projects.Cores.DynamicMiddleware.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Projects.Cores.MicroClient.Options
{
    /// <summary>
    /// 客户端代理选项
    /// </summary>
    public class MicroClientOptions
    {

        public MicroClientOptions()
        {
            dynamicMiddlewareOptions = options => { };
        }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// 动态中台选项
        /// </summary>
        public Action<DynamicMiddlewareOptions> dynamicMiddlewareOptions { get; set; }
    }
}
