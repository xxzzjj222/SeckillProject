using Castle.DynamicProxy.Internal;
using Microsoft.Extensions.DependencyModel;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Projects.Cores.MicroClient
{
    /// <summary>
    /// 程序集工具类
    /// </summary>
    public class AssemblyUtil
    {
        /// <summary>
        /// 获取项目程序集，排除所有的系统程序集（Microsoft.***,System.***,Nuget下载包）
        /// </summary>
        /// <returns></returns>
        public static IList<Assembly> GetAssemblies()
        {
            List<Assembly> assemblies = new List<Assembly>();

            var deps = DependencyContext.Default;
            var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");
            foreach(var lib in libs)
            {
                try
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                    assemblies.Add(assembly);
                }
                catch(Exception ex)
                {
                    //
                }
            }

            return assemblies;
        }

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static Assembly GetAssembly(string assemblyName)
        {
            //return GetAssemblies().FirstOrDefault(f => f.FullName.Contains(assemblyName));
            return AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
        }

        /// <summary>
        /// 获取所有类型
        /// </summary>
        /// <returns></returns>
        public static IList<Type> GetTypes()
        {
            List<Type> types = new List<Type>();
            foreach(var assembly in GetAssemblies())
            {
                foreach(var typeinfo in assembly.DefinedTypes)
                {
                    types.Add(typeinfo.AsType());
                }
            }
            return types;
        }

        /// <summary>
        /// 获取程序集所有类型
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static IList<Type> GetTypesByAssembly(string assemblyName)
        {
            List<Type> types = new List<Type>();
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
            types = assembly.GetTypes().ToList();
            return types;
        }

        /// <summary>
        /// 获取包含MicroClient特性的所有类型
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static IList<Type> GetMicroClientTypesByAssembly(string assemblyName)
        {
            List<Type> types = new List<Type>();
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
            var typeInfos = assembly.DefinedTypes;
            foreach(var typeInfo in typeInfos)
            {
                if(typeInfo.GetCustomAttribute<MicroClient.Attributes.MicroClient>()!=null)
                {
                    types.Add(typeInfo.AsType());
                }
            }
            return types;
        }

        /// <summary>
        /// 获取实例化类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="baseInterfaceType"></param>
        /// <returns></returns>
        public static Type GetImplementType(string typeName,Type baseInterfaceType)
        {
            return GetTypes().FirstOrDefault(t =>
            {
                if (t.Name == typeName && t.GetTypeInfo().GetAllInterfaces().Any(b => b.Name == baseInterfaceType.Name))
                {
                    var typeInfo = t.GetTypeInfo();
                    return typeInfo.IsClass && !typeInfo.IsAbstract && !typeInfo.IsGenericType;
                }
                return false;
            });
        }
    }
}
