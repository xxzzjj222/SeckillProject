using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.PaymentServices.Payments
{
    /// <summary>
    /// 支付评价接口
    /// </summary>
    public interface IPayComment
    {
        /// <summary>
        /// 支付订单评价查询
        /// </summary>
        /// <param name="begin_time">begin_time(begin_time)[按用户评论时间批量拉取的起始时间，格式为yyyyMMddHHmmss]</param>
        /// <param name="end_time">end_time(end_time)[按用户评论时间批量拉取的结束时间，格式为yyyyMMddHHmmss]</param>
        /// <param name="offset">offset(位移)[指定从某条记录的下一条开始返回记录。接口调用成功时，会返回本次查询最后一条数据的offset。商户需要翻页时，应该把本次调用返回的offset 作为下次调用的入参。注意offset是评论数据在微信支付后台保存的索引，未必是连续的]</param>
        /// <returns></returns>
        public IDictionary<string, object> BatchQueryComment(string begin_time, string end_time, string offset);

        /// <summary>
        /// 支付订单评价查询
        /// </summary>
        /// <param name="begin_time">begin_time(begin_time)[按用户评论时间批量拉取的起始时间，格式为yyyyMMddHHmmss]</param>
        /// <param name="end_time">end_time(end_time)[按用户评论时间批量拉取的结束时间，格式为yyyyMMddHHmmss]</param>
        /// <param name="offset">offset(位移)[指定从某条记录的下一条开始返回记录。接口调用成功时，会返回本次查询最后一条数据的offset。商户需要翻页时，应该把本次调用返回的offset 作为下次调用的入参。注意offset是评论数据在微信支付后台保存的索引，未必是连续的]</param>
        /// <param name="limit">limit(一次拉取的条数, 最大值是200，默认是200)</param>
        /// <returns></returns>
        public IDictionary<string, object> BatchQueryComment(string begin_time, string end_time, string offset, string limit);
    }
}
