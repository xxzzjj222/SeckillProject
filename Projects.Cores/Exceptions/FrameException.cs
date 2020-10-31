using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.Exceptions
{
    /// <summary>
    /// 框架异常
    /// </summary>
    public class FrameException:Exception
    {
        /// <summary>
        /// 业务异常编号
        /// </summary>
        public string ErrorNo { get; }

        /// <summary>
        /// 业务异常信息
        /// </summary>
        public string ErrorInfo { get; }

        /// <summary>
        /// 业务异常详细信息
        /// </summary>
        public IDictionary<string,object> Infos { get; set; }

        public FrameException(string errorNo,string errorInfo)
            :base(errorInfo)
        {
            ErrorNo = errorNo;
            ErrorInfo = errorInfo;
        }

        public FrameException(string errorNo, string errorInfo, Exception e) : base(errorInfo, e)
        {
            ErrorNo = errorNo;
            ErrorInfo = errorInfo;
        }

        public FrameException(string errorInfo) : base(errorInfo)
        {
            ErrorNo = "-1";
            ErrorInfo = errorInfo;
        }

        public FrameException(string errorInfo, Exception e) : base(errorInfo, e)
        {
            ErrorNo = "-1";
            ErrorInfo = errorInfo;
        }

        public FrameException(Exception e)
        {
            ErrorNo = "-1";
            ErrorInfo = e.Message;
        }
    }
}
