using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.PaymentServices.Payments.Wx
{
    public class WxPay : IScenePay
    {
        /// <summary>
        /// 微信支付链接
        /// </summary>
        private const string order_create_url = "https://api.mch.weixin.qq.com/pay/unifiedorder";   //微信创建订单接口
        private const string order_query_url = "https://api.mch.weixin.qq.com/pay/orderquery";   //微信订单查询
        private const string order_close_url = "https://api.mch.weixin.qq.com/pay/closeorder";   //微信订单关闭
        private const string order_refund_url = "https://api.mch.weixin.qq.com/secapi/pay/refund";   //微信退款申请
        private const string order_refund_query_url = "https://api.mch.weixin.qq.com/pay/refundquery";   //微信退款查询
        private const string order_download_bill = "https://api.mch.weixin.qq.com/pay/downloadbill";   //微信对账单下载
        private const string order_download_fund_flow = "https://api.mch.weixin.qq.com/pay/downloadfundflow";   //微信资金账单下载
        private const string order_batch_query_comment = "https://api.mch.weixin.qq.com/billcommentsp/batchquerycomment";   //微信订单评价查询

        public IDictionary<string, object> CreateJSAPIOrder(string order_sn, string total_fee, string openid)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> CreateJSAPIOrder(string order_sn, string total_fee, string openid, string body)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> CreateNATIVEOrder(string order_sn, string total_fee)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> CreateNATIVEOrder(string order_sn, string total_fee, string body)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> CreateOrder(IDictionary<string, object> requestParam)
        {
            throw new NotImplementedException();
        }
    }
}
