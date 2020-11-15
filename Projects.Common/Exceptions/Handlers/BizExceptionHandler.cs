using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;


namespace Projects.Common.Exceptions.Handlers
{
    public class BizExceptionHandler:IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // 1、判断异常是否BizException
            if (context.Exception is BizException bizException)
            {
                // 1.1 将异常转换成为json结果
                dynamic exceptionResult = new ExpandoObject();
                exceptionResult.ErrorNo = bizException.ErrorNo;
                exceptionResult.ErrorInfo = bizException.ErrorInfo;
                if (bizException.Infos != null)
                {
                    exceptionResult.infos = bizException.Infos;
                }
                context.Result = new JsonResult(exceptionResult);
            }
            else
            {
                // 1.2 处理其他类型异常Exception
                dynamic exceptionResult = new ExpandoObject();
                exceptionResult.ErrorNo = -1;
                exceptionResult.ErrorInfo = context.Exception.Message;

                // 1.3 包装异常信息进行异常返回
                context.Result = new JsonResult(exceptionResult);
            }
        }
    }
}
