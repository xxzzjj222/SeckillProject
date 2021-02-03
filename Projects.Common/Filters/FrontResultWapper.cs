using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Common.Filters
{
    /// <summary>
    /// 页面调用结果包装器
    /// </summary>
    public class FrontResultWapper : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if(context.Result is ObjectResult objectResult)
            {
                int? statusCode= objectResult.StatusCode;
                if (statusCode == 200 || statusCode == 201 || statusCode == 202 || !statusCode.HasValue)
                    objectResult.Value = WrapSuccessResult(objectResult.Value);
                else
                    objectResult.Value = WrapFailResult(objectResult);
            }
            await next();
        }

        private object WrapFailResult(ObjectResult objectResult)
        {
            dynamic wrapResult = new ExpandoObject();
            wrapResult.ErrorNo = objectResult.StatusCode;
            if(objectResult.Value is string info)
            {
                wrapResult.ErrorInfo = info;
            }
            else
            {
                wrapResult.ErrorInfo = new JsonResult(objectResult.Value).Value;
            }

            return wrapResult;
        }

        private object WrapSuccessResult(object value)
        {
            dynamic wrapResult = new ExpandoObject();

            wrapResult.ErrorNo = "0";
            wrapResult.ErrorInfo = "";

            if(value.GetType().Name.Contains("List"))
            {
                wrapResult.ResultList = new JsonResult(value).Value;
            }
            else if(value.GetType().Name.Contains("Dictionary"))
            {
                IDictionary dictionary = (IDictionary)value;
                if(dictionary.Contains("ErrorInfo"))
                {
                    wrapResult.ErrorNo = dictionary["ErrorNo"];
                    wrapResult.ErrorInfo = dictionary["ErrorInfo"];

                    dictionary.Remove("ErrorNo");
                    dictionary.Remove("ErrorInfo");
                }
                wrapResult.ResultDic = new JsonResult(value).Value;
            }
            else
            {
                wrapResult.ResultDic = new JsonResult(value).Value;
            }

            return wrapResult;
        }
    }
}
