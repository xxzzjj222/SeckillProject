using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Common.Exceptions
{
    public class BizException:Exception
    {
        public string ErrorNo { get; } // 业务异常编号
        public string ErrorInfo { get; }// 业务异常信息

        public IDictionary<string, object> Infos { set; get; } // 业务异常详细信息

        public BizException(string errorNo, string errorInfo) : base(errorInfo)
        {
            ErrorNo = errorNo;
            ErrorInfo = errorInfo;
        }

        public BizException(string errorNo, string errorInfo, Exception e) : base(errorInfo, e)
        {
            ErrorNo = errorNo;
            ErrorInfo = errorInfo;
        }

        public BizException(string errorInfo) : base(errorInfo)
        {
            ErrorNo = "-1";
            ErrorInfo = errorInfo;
        }

        public BizException(string errorInfo, Exception e) : base(errorInfo, e)
        {
            ErrorNo = "-1";
            ErrorInfo = errorInfo;
        }

        public BizException(Exception e)
        {
            ErrorNo = "-1";
            ErrorInfo = e.Message;
        }
    }
}
