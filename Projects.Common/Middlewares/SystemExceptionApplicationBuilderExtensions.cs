using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Common.Middlewares
{
    /// <summary>
    /// 自定义系统异常中间件
    /// </summary>
    public static class SystemExceptionApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSystemException(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<SystemExceptionHandlerMiddleware>();
            return builder;
        }
    }
}
