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
    /// 通用结果包装器
    /// </summary>
    public class MiddlewareResultWapper : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult)
            {
                int? StatusCode = objectResult.StatusCode;
                if (StatusCode == 200
                    || StatusCode == 201
                    || StatusCode == 202
                    || !StatusCode.HasValue)
                {
                    // 1、包装正常结果
                    objectResult.Value = WrapSuccessResult(objectResult.Value);
                }
                else
                {
                    // 2、包装异常结果
                    objectResult.Value = WrapFailResult(objectResult);
                }
            }
            await next();
        }

        private object WrapFailResult(ObjectResult objectResult)
        {
            dynamic warpResult = new ExpandoObject();
            warpResult.ErrorNo = objectResult.StatusCode;
            if (objectResult.Value is string info)
            {
                // 1、字符串异常信息
                warpResult.ErrorInfo = info;
            }
            else
            {
                // 2、类型异常信息
                warpResult.ErrorInfo = new JsonResult(objectResult.Value).Value;
            }

            return warpResult;
        }

        private object WrapSuccessResult(object value)
        {
            // 1、创建返回结果
            dynamic warpResult = new ExpandoObject();
            warpResult.ErrorNo = "0";
            warpResult.ErrorInfo = "";

            // 2、判断是否为字典
            if (value.GetType().Name.Contains("Dictionary"))
            {
                //2.1 判断是否含有ErrorInfo
                IDictionary dictionary = (IDictionary)value;
                if (dictionary.Contains("ErrorInfo"))
                {
                    warpResult.ErrorNo = dictionary["ErrorNo"];
                    warpResult.ErrorInfo = dictionary["ErrorInfo"];

                    // 2.2 删除字典里面的ErrorNo，ErrorInfo
                    dictionary.Remove("ErrorNo");
                    dictionary.Remove("ErrorInfo");
                }
            }

            // 3、获取结果
            warpResult.Result = new JsonResult(value).Value;

            return warpResult;
        }
    }
}
