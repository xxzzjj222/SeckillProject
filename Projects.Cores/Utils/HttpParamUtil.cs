using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.Utils
{
    /// <summary>
    /// http参数工具类
    /// </summary>
    public class HttpParamUtil
    {
        /// <summary>
        /// 字典转换成url参数 ?userid=1&name=xx
        /// </summary>
        /// <param name="middleParam"></param>
        /// <returns></returns>
        public static string DicToHttpUrlParam(IDictionary<string,object> middleParam)
        {
            if (middleParam.Count!=0)
            {
                //1.拼接
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("?");
                foreach (var keyValue in middleParam)
                {
                    stringBuilder.Append(keyValue.Key);
                    stringBuilder.Append("=");
                    stringBuilder.Append(keyValue.Value);
                    stringBuilder.Append("&");
                }
                //2.移除最后一个&
                string urlParam = stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString();
                return urlParam;
            }
            return "";
        }
    }
}
