using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.MicroClient.Attributes
{
    /// <summary>
    /// Post请求特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PostPath:Attribute
    {
        public PostPath(string path)
        {
            Path = path;
        }

        /// <summary>
        /// 请求路径
        /// </summary>
        public string Path { get; }
    }
}
