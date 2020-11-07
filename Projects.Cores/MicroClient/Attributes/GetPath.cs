using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.MicroClient.Attributes
{
    /// <summary>
    /// Get方法请求
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GetPath:Attribute
    {
        public GetPath(string path)
        {
            Path = path;
        }
        /// <summary>
        /// 请求路径
        /// </summary>
        public string Path { get; set; }
    }
}
