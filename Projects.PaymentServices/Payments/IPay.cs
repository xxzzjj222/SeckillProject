using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.PaymentServices.Payments
{
    /// <summary>
    /// 支付接口
    /// </summary>
    public interface IPay
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        public IDictionary<string, object> CreateOrder(IDictionary<string, object> requestParam);
    }
}
