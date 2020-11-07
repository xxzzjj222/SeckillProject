using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.MicroClient.Attributes
{
    /// <summary>
    /// 路径变量特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class PathVariable:Attribute
    {
        public PathVariable(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
