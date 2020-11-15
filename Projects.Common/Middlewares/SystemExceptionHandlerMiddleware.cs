using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Projects.Common.Middlewares
{
    public class SystemExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public SystemExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandlerExceptionAsync(context, context.Response.StatusCode, ex.Message);
            }

        }

        private async static Task HandlerExceptionAsync(HttpContext context,int statusCode,string msg)
        {
            context.Response.ContentType = "application/json;charset=utf-8";

            dynamic wrapResult = new ExpandoObject();
            wrapResult.ErrorNo = statusCode;
            wrapResult.ErrorInfo = msg;

            var stream = context.Response.Body;
            await JsonSerializer.Serialize(stream, wrapResult);
        }
    }
}
