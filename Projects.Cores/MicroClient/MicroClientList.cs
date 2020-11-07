using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Projects.Cores.MicroClient
{
    public class MicroClientList
    {
        private readonly MicroClientProxyFactory _microClientProxyFactory;
        
        public MicroClientList(MicroClientProxyFactory microClientProxyFactory)
        {
            _microClientProxyFactory = microClientProxyFactory;
        }

        /// <summary>
        /// 获取所有客户端实例
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public IDictionary<Type,object> GetClients(string assemblyName)
        {
            //加载所有MicroClient类型
            IList<Type> types = AssemblyUtil.GetMicroClientTypesByAssembly(assemblyName);
            //创建所有对象实例
            IDictionary<Type, object> keyValuePairs = new Dictionary<Type, object>();
            foreach(var type in types)
            {
                object value = _microClientProxyFactory.CreateMircoClientProxy(type);
                keyValuePairs.Add(type, value);
            }
            return keyValuePairs;
        }
    }
}
