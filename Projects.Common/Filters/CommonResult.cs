using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Common.Filters
{
    public class CommonResult
    {
        public const string SUCCESS = "0";

        public string ErrorNo { get; set; }

        public string ErrorInfo { get; set; }
        public IDictionary<string,object> resultDic { get; set; }//用于非结果集返回

        public IList<IDictionary<string,object>> resultList { get; set; }//用于结果集返回
        public dynamic Result { get; set; }//返回动态结果（通用）

        public CommonResult()
        {
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }

        public CommonResult(string errorNo, string errorInfo)
        {
            ErrorNo = errorNo;
            ErrorInfo = errorInfo;
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }

        public CommonResult(string errorNo, string errorInfo, IDictionary<string, object> resultDic, IList<IDictionary<string, object>> resultList) : this(errorNo, errorInfo)
        {
            this.resultDic = resultDic;
            this.resultList = resultList;
        }
    }
}
