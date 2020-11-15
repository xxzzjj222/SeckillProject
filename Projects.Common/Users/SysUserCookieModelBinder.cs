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
    /// 系统用户模型绑定(从Cookie获取用户信息，UserId和UserName)
    /// 1、将HttpContext用户信息转换成为Sysuser
    /// 2、将Sysuser绑定到action方法参数
    /// </summary>
    public class SysUserCookieModelBinder:IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            if (bindingContext.ModelType == typeof(SysUser))
            {
                // 1、创建用户对象
                SysUser sysUser = new SysUser();

                // 2、设置模型值(cookie，header)
                HttpContext httpContext = bindingContext.HttpContext;
                IRequestCookieCollection cookies = httpContext.Request.Cookies;
                string UserId = cookies["UserId"];
                string UserName = cookies["UserName"];
                // 1、判断UserId是否为空
                if (string.IsNullOrEmpty(UserId))
                {
                    throw new BizException("授权失败，没有登录");
                }
                sysUser.UserId = Convert.ToInt32(UserId);
                sysUser.UserName = UserName;

                // 3、返回结果
                bindingContext.Result = ModelBindingResult.Success(sysUser);

            }

            return Task.CompletedTask;
        }
    }
}
