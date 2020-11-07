using Castle.DynamicProxy;
using Newtonsoft.Json;
using Projects.Cores.DynamicMiddleware;
using Projects.Cores.Exceptions;
using Projects.Cores.MicroClient.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Projects.Cores.MicroClient
{
    public class MicroClientProxy : IInterceptor
    {
        private readonly IDynamicMiddlewareService _dynamicMiddlewareService;

        public MicroClientProxy(IDynamicMiddlewareService dynamicMiddlewareService)
        {
            _dynamicMiddlewareService = dynamicMiddlewareService;
        }

        /// <summary>
        /// 客户端代理执行
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            //1.获取接口方法
            var methodInfo = invocation.Method;

            //2.获取方法上特性
            var attributes = methodInfo.GetCustomAttributes();

            //3.遍历
            foreach (var attribute in attributes)
            {
                //1.获取url
                var type = methodInfo.DeclaringType;
                MicroClient.Attributes.MicroClient microClient = type.GetCustomAttribute<MicroClient.Attributes.MicroClient>();
                if (microClient == null)
                    throw new FrameException("MicroClient 特性不能为空");

                //2.转换成动态参数
                ProxyMethodParameter proxyMethodParameter = new ProxyMethodParameter(methodInfo.GetParameters(), invocation.Arguments);
                dynamic paramPairs = ArgumentConvert(proxyMethodParameter);

                if(attribute is GetPath getPath)
                {
                    //路径变量转换
                    string path = PathParse(getPath.Path, paramPairs);
                    //执行
                    dynamic result = _dynamicMiddlewareService.GetDynamic(microClient.UrlScheme, microClient.ServiceName, path, paramPairs);
                    //获取返回值类型
                    Type returnType = methodInfo.ReturnType;
                    //赋值给返回值
                    invocation.ReturnValue = ResultConvert(result, returnType);
                }
                else if(attribute is PutPath putPath)
                {
                    //路径变量转换
                    string path = PathParse(putPath.Path, paramPairs);
                    //执行
                    dynamic result = _dynamicMiddlewareService.PutDynamic(microClient.UrlScheme, microClient.ServiceName, path, paramPairs);
                    //获取返回值类型
                    Type returnType = methodInfo.ReturnType;
                    //赋值给返回值
                    invocation.ReturnValue = ResultConvert(result, returnType);
                }
                else if(attribute is PostPath postPath)
                {
                    //路径变量转换
                    string path = PathParse(postPath.Path, paramPairs);
                    //执行
                    dynamic result = _dynamicMiddlewareService.PostDynamic(microClient.UrlScheme, microClient.ServiceName, path, paramPairs);
                    //获取返回值类型
                    Type returnType = methodInfo.ReturnType;
                    //赋值给返回值
                    invocation.ReturnValue = ResultConvert(result, returnType);
                }
                else if(attribute is DeletePath deletePath)
                {
                    //路径变量转换
                    string path = PathParse(deletePath.Path, paramPairs);
                    //执行
                    dynamic result = _dynamicMiddlewareService.DeleteDynamic(microClient.UrlScheme, microClient.ServiceName, path, paramPairs);
                    //获取返回值类型
                    Type returnType = methodInfo.ReturnType;
                    //赋值给返回值
                    invocation.ReturnValue = ResultConvert(result, returnType);
                }
                else
                {
                    throw new FrameException("特性方法不存在");
                }

            }
        }

        /// <summary>
        /// 参数转换为动态类型
        /// </summary>
        /// <param name="proxyMethodParameter"></param>
        /// <returns></returns>
        private dynamic ArgumentConvert(ProxyMethodParameter proxyMethodParameter)
        {
            //动态参数
            dynamic dynamicParams = new Dictionary<string, object>();

            //多个参数情况包装成字典
            IDictionary<string, object> paramPairs = new Dictionary<string, object>();
            foreach (var parameterInfo in proxyMethodParameter.ParameterInfos)
            {
                object parameterValue = proxyMethodParameter.Arguments[parameterInfo.Position];
                Type parameterType = parameterInfo.ParameterType;

                //1.是否一个参数
                if (proxyMethodParameter.Arguments.Length==1)
                {
                    //如果是值类型
                    if(parameterType.IsValueType)
                    {
                        PathVariable pathVariable = parameterInfo.GetCustomAttribute<PathVariable>();
                        if (pathVariable != null)
                            //设置路径变量名称
                            paramPairs.Add(pathVariable.Name, proxyMethodParameter.Arguments[parameterInfo.Position]);
                        else
                            paramPairs.Add(parameterInfo.Name, proxyMethodParameter.Arguments[parameterInfo.Position]);
                        dynamicParams = paramPairs;
                    }
                    else
                    {
                        //引用类型，直接动态返回
                        dynamicParams = parameterValue;
                    }
                }
                else
                {
                    //两个及两个以上，全部用字典组装起来
                    PathVariable pathVariable = parameterInfo.GetCustomAttribute<PathVariable>();
                    if (pathVariable != null)
                        paramPairs.Add(pathVariable.Name, proxyMethodParameter.Arguments[parameterInfo.Position]);
                    else
                        paramPairs.Add(parameterInfo.Name, proxyMethodParameter.Arguments[parameterInfo.Position]);
                    dynamicParams = paramPairs;
                }
            }
            return dynamicParams;
        }

        /// <summary>
        /// 路径变量解析
        /// </summary>
        /// <param name="path"></param>
        /// <param name="paramPairs"></param>
        /// <returns></returns>
        private string PathParse(string path,dynamic paramPairs)
        {
            if(paramPairs is IDictionary<string,object>)
            {
                //路径前缀
                string pathPrefi = "{";
                //路径后缀
                string pathSuffix = "}";

                foreach(var key in paramPairs.Keys)
                {
                    string pathVariable = pathPrefi + key + pathSuffix;
                    if(path.Contains(pathVariable))
                    {
                        path = path.Replace(pathVariable, Convert.ToString(paramPairs[key]));
                    }
                }
            }
            return path;
        }

        /// <summary>
        /// 结果转换
        /// </summary>
        /// <param name="result"></param>
        /// <param name="convertType"></param>
        /// <returns></returns>
        private dynamic ResultConvert(dynamic result,Type convertType)
        {
            if(convertType==typeof(void))
            {
                return null;
            }
            string resultJson = JsonConvert.SerializeObject(result);
            dynamic returnResult = JsonConvert.DeserializeObject(resultJson, convertType);
            return returnResult;
        }

    }

    class ProxyMethodParameter
    {
        //参数类型
        public ParameterInfo[] ParameterInfos { get; }

        //参数值
        public object[] Arguments { get; }

        public ProxyMethodParameter(ParameterInfo[] parameterInfos, object[] arguments)
        {
            ParameterInfos = parameterInfos;
            Arguments = arguments;
        }
    }
}
