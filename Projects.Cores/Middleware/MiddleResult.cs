using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;

namespace Projects.Cores.Middleware
{
    /// <summary>
    /// 中台结果
    /// </summary>
    public class MiddleResult
    {
        public const string Success = "0";
        public string ErrorNo { get; set; }//是否成功状态
        public string ErrorInfo { get; set; }//失败信息
        public IDictionary<string,object> resultDic { get; set; }//用于非结果集返回
        public IList<IDictionary<string,object>> resultList { get; set; }//用于结果集返回
        public dynamic Result { get; set; }//返回动态结果（通用）
        public MiddleResult()
        {
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }

        public MiddleResult(string jsonStr)
        {
            Result = JsonConvert.DeserializeObject<MiddleResult>(jsonStr);         
        }

        public static MiddleResult JsonToMiddleResult(string jsonStr)
        {
            MiddleResult result = JsonConvert.DeserializeObject<MiddleResult>(jsonStr);
            return result;
        }

        public MiddleResult(string errorNo,string errorInfo)
        {
            ErrorNo = errorNo;
            ErrorInfo = errorInfo;
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }

        public MiddleResult(string errorNo,string errorInfo,IDictionary<string,object> resultDic,IList<IDictionary<string,object>> resultList)
            :this(errorNo,errorInfo)
        {
            this.resultDic = resultDic;
            this.resultList = resultList;
        }
    }
}
