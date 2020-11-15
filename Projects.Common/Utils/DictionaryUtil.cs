using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Projects.Common.Utils
{
    /// <summary>
    /// 字典工具类
    /// </summary>
    public class DictionaryUtil
    {
        /// <summary>
        /// 对象转换成字典
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDictionary<string, object> ToDictionary(object value)
        {
            IDictionary<string, object> valuePairs = new Dictionary<string, object>();

            Type type = value.GetType();

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach(var property in properties)
            {
                valuePairs.Add(property.Name, Convert.ToString(property.GetValue(value)));
            }
            return valuePairs;
        }
    }
}
