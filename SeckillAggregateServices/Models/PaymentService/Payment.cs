using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Models.PaymentService
{
    /// <summary>
    /// 支付模型
    /// </summary>
    public class Payment
    {
        public int Id { set; get; } //  '支付编号',
        public decimal PaymentPrice { set; get; } //  '支付金额',
        public int PaymentStatus { set; get; } //  '支付状态',
        public int OrderId { set; get; } //  '订单主键',
        public string OrderSn { set; get; } //  '订单号',
        public int PaymentType { set; get; }// 支付类型
        public string PaymentMethod { set; get; } //  '支付方式',
        public DateTime Createtime { set; get; } //  '支付创建时间',
        public DateTime Updatetime { set; get; } //  '支付更新时间',
        public string PaymentRemark { set; get; } //  '支付备注',
        public string PaymentUrl { set; get; } //  '支付url',
        public string PaymentReturnUrl { set; get; } //  '支付回调url',
        public string PaymentCode { set; get; }//  '支付单号',
        public int UserId { set; get; } //  '用户Id',
        public string PaymentErrorNo { set; get; } //  '支付错误编号',
        public string PaymentErrorInfo { set; get; } //  '支付错误信息',
    }
}
