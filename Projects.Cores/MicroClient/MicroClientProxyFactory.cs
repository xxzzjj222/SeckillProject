using Castle.DynamicProxy;
using Projects.Cores.DynamicMiddleware;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.MicroClient
{
    public class MicroClientProxyFactory
    {
        private readonly IDynamicMiddlewareService _dynamicMiddlewareService;

        public MicroClientProxyFactory(IDynamicMiddlewareService dynamicMiddlewareService)
        {
            _dynamicMiddlewareService = dynamicMiddlewareService;
        }

        /// <summary>
        /// 创建接口代理类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object CreateMircoClientProxy(Type type)
        {
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            object t = proxyGenerator.CreateInterfaceProxyWithoutTarget(type, new MicroClientProxy(_dynamicMiddlewareService));
            return t;
        }
    }
}
