using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Common.Utils
{
    /// <summary>
    /// 订单号工具类(单机)
    /// </summary>
    public class OrderUtil
    {
        //订单编号前缀
        private const string PREFIX = "Prefix";
        //订单编号后缀（核心部分）
        private static long code;
        private static object lockObject = new object();
        // 生成订单编号
        public static string GetOrderCode()
        {
            lock (lockObject)
            {
                code++;
                string str = string.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
                long m = long.Parse((str)) * 10000;
                m += code;
                return PREFIX + m;
            }
        }
    }
}
