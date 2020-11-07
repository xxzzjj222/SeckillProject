using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.MicroClient.Attributes
{
    /// <summary>
    /// Put请求特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PutPath:Attribute
    {
        public PutPath(string path)
        {
            Path = path;
        }

        /// <summary>
        /// 请求路径
        /// </summary>
        public string Path { get; }
    }
}
