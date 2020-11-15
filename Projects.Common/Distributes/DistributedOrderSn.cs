using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Common.Distributes
{
    /// <summary>
    /// 分布式订单
    /// </summary>
    public class DistributedOrderSn
    {
        private readonly SnowflakeId snowflakeId;

        public DistributedOrderSn(SnowflakeId snowflakeId)
        {
            this.snowflakeId = snowflakeId;
        }

        /// <summary>
        /// 创建订单号
        /// </summary>
        /// <returns></returns>
        public string CreateDistributedOrderSn()
        {
            // 1、可以选择加前缀
            return Convert.ToString(snowflakeId.NextId());
        }
    }
}
