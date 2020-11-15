using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Projects.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Common.Users
{
    /// <summary>
    /// 系统用户模型绑定(从头部信息获取用户信息，UserId和UserName)
    /// 1、将HttpContext用户信息转换成为Sysuser
    /// 2、将Sysuser绑定到action方法参数
    /// </summary>
    public class SysUserHeaderModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(bindingContext==null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            if(bindingContext.ModelType==typeof(SysUser))
            {
                SysUser sysUser = new SysUser();

                HttpContext context = bindingContext.HttpContext;
                var header = context.Request.Headers;
                string UserId = header["UserId"];
                string UserName = header["UserName"];
                if(string.IsNullOrEmpty(UserId))
                {
                    throw new BizException("授权失败，没有登录");
                }
                sysUser.UserId = int.Parse(UserId);
                sysUser.UserName = UserName;
                bindingContext.Result = ModelBindingResult.Success(sysUser);
            }
            return Task.CompletedTask;
        }
    }
}
