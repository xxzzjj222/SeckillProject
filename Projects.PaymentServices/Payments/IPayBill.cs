using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.PaymentServices.Payments
{
    /// <summary>
    /// 支付账单接口
    /// </summary>
    public interface IPayBill
    {
        /// <summary>
        /// 支付账单下载
        /// </summary>
        /// <param name="bill_data">(对账单日期)[下载对账单的日期，格式：20140603]</param>
        /// <param name="bill_type">(账单类型)[ ALL，返回当日所有订单信息，默认值
        /// SUCCESS，返回当日成功支付的订单
        /// REFUND，返回当日退款订单
        /// RECHARGE_REFUND，返回当日充值退款订单]</param>
        /// <param name="tar_type">tar_type(压缩账单)[非必传参数，固定值：GZIP，返回格式为.gzip的压缩包账单。不传则默认为数据流形式。]</param>
        /// <returns></returns>
        public byte[] DownloadBill(string bill_data, string bill_type, string tar_type);

        /// <summary>
        /// 支付订单资金账单下载
        /// </summary>
        /// <param name="bill_date">(对账单日期)[下载对账单的日期，格式：20140603]</param>
        /// <param name="account_type">[ 账单的资金来源账户：
        /// Basic 基本账户
        /// Operation 运营账户
        /// Fees 手续费账户]</param>
        /// <param name="tar_type"></param>
        /// <returns></returns>
        public byte[] DownloadRundFlow(string bill_data, string account_type, string tar_type);
    }
}
