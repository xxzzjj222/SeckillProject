using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.Middleware
{
    /// <summary>
    /// 中台服务
    /// </summary>
    public interface IMiddleService
    {
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public MiddleResult Get(string middleUrl, IDictionary<string, object> middleParam);

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public MiddleResult Post(string middleUrl, IDictionary<string, object> middleParam);

        /// <summary>
        /// Post请求,动态参数
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public MiddleResult PostDynamic(string middleUrl, dynamic middleParam);

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParams">中台集合参数</param>
        /// <returns></returns>
        public MiddleResult Post(string middleUrl, IList<IDictionary<string, object>> middleParams);

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public MiddleResult Delete(string middleUrl, IDictionary<string, object> middleParam);

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public MiddleResult Put(string middleUrl, IDictionary<string, object> middleParam);

        /// <summary>
        /// Put请求，动态参数
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public MiddleResult PutDynamic(string middleUrl, dynamic middleParam);

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParams">中台集合参数</param>
        /// <returns></returns>
        public MiddleResult Put(string middleUrl, IList<IDictionary<string, object>> middleParams);
    }
}
