using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.MicroClient.Attributes
{
    /// <summary>
    /// Delete请求特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DeletePath:Attribute
    {
        public DeletePath(string path)
        {
            Path = path;
        }

        /// <summary>
        /// 请求路径
        /// </summary>
        public string Path { get; }
    }
}
