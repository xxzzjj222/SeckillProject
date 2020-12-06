using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.PaymentServices.Payments
{
    /// <summary>
    /// 场景订单支付接口
    /// </summary>
    public interface IScenePay:IPay
    {
        /// <summary>
        /// 创建扫码订单[pc端支付]
        /// </summary>
        /// <param name="order_sn">订单号</param>
        /// <param name="total_fee">总金额</param>
        /// <returns></returns>
        public IDictionary<string, object> CreateNATIVEOrder(string order_sn, string total_fee);

        /// <summary>
        /// 创建扫码订单[pc端支付]
        /// </summary>
        /// <param name="order_sn">订单号</param>
        /// <param name="total_fee">总金额</param>
        /// <param name="body">body(商品描述)[商品描述交易字段格式根据不同的应用场景按照以下格式：
        /// APP——需传入应用市场上的APP名字-实际商品名称，天天爱消除-游戏充值。]</param>
        /// <returns></returns>
        public IDictionary<string, object> CreateNATIVEOrder(string order_sn, string total_fee, string body);

        /// <summary>
        /// 创建JSAPI订单[移动端支付]
        /// </summary>
        /// <param name="order_sn">订单号</param>
        /// <param name="total_fee">总金额</param>
        /// <param name="openid">trade_type=JSAPI时（即公众号支付），此参数必传，此参数为微信用户在商户对应appid下的唯一标识。openid如何获取，可参考【获取openid】。企业号请使用【企业号OAuth2.0接口】获取企业号内成员userid，再调用【企业号userid转openid接口】进行转换</param>
        /// <returns></returns>
        public IDictionary<string, object> CreateJSAPIOrder(string order_sn, string total_fee, string openid);

        /// <summary>
        /// 创建JSAPI订单[移动端支付]
        /// </summary>
        /// <param name="order_sn">订单号</param>
        /// <param name="total_fee">总金额</param>
        /// <param name="openid">trade_type=JSAPI时（即公众号支付），此参数必传，此参数为微信用户在商户对应appid下的唯一标识。openid如何获取，可参考【获取openid】。企业号请使用【企业号OAuth2.0接口】获取企业号内成员userid，再调用【企业号userid转openid接口】进行转换</param>
        /// <param name="body">(商品描述)[商品描述交易字段格式根据不同的应用场景按照以下格式：
        /// APP——需传入应用市场上的APP名字-实际商品名称，天天爱消除-游戏充值。]</param>
        /// <returns></returns>
        public IDictionary<string, object> CreateJSAPIOrder(string order_sn, string total_fee, string openid, string body);
    }
}
