using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.PaymentServices.Payments
{
    /// <summary>
    /// 支付退款接口
    /// </summary>
    public interface IPayRefund
    {
        /// <summary>
        /// 支付订单退款申请
        /// </summary>
        /// <param name="order_sn">订单号</param>
        /// <param name="out_refund_no">退款单号</param>
        /// <param name="total_fee">订单金额</param>
        /// <param name="refund_fee">退款金额</param>
        /// <returns></returns>
        public IDictionary<string, object> Refund(string order_sn, string out_refund_no, string tatal_fee, string refund_fee);

        /// <summary>
        /// 支付订单退款申请
        /// </summary>
        /// <param name="order_sn">订单号</param>
        /// <param name="out_refund_no">退款单号</param>
        /// <param name="total_fee">订单金额</param>
        /// <param name="refund_fee">退款金额</param>
        /// <param name="refund_desc">退款原因</param>
        /// <returns></returns>
        public IDictionary<string, object> Refund(string order_sn, string out_refund_no, string total_fee, string refund_fee, string refund_desc);

        /// <summary>
        /// 支付订单退款申请
        /// </summary>
        /// <param name="request_map"></param>
        /// <returns></returns>
        public IDictionary<string, object> Refund(IDictionary<string, object> request_map);

        /// <summary>
        /// 支付订单退款查询
        /// </summary>
        /// <param name="order_sn">订单号</param>
        /// <returns></returns>
        public IDictionary<string, object> RefundQuery(string order_sn);

        /// <summary>
        /// 支付订单退款查询
        /// </summary>
        /// <param name="order_sn">订单号</param>
        /// <param name="offset">(偏移量)[偏移量，当部分退款次数超过10次时可使用，表示返回的查询结果从这个偏移量开始取记录]</param>
        /// <returns></returns>
        public IDictionary<string, object> RefundQuery(string order_sn, int offset);

        /// <summary>
        /// 支付订单退款查询
        /// </summary>
        /// <param name="request_map">退款查询参数</param>
        /// <returns></returns>
        public IDictionary<string, object> RefundQuery(IDictionary<string, object> request_map);
    }
}
