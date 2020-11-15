using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Projects.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Common.Users
{
    /// <summary>
    /// 系统用户模型绑定
    /// 将HttpContext用户信息转换为SysUser
    /// 将SysUser绑定到action方法参数
    /// </summary>
    public class SysUserModelBinder : IModelBinder
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

                HttpContext httpContext = bindingContext.HttpContext;
                ClaimsPrincipal claimsPrincipal = httpContext.User;
                IEnumerable<Claim> claims = claimsPrincipal.Claims;

                if(claims.ToList().Count==0)
                {
                    throw new BizException("授权失败，没有登录");
                }
                foreach(var claim in claims)
                {
                    if(claim.Type.Equals("sub"))
                    {
                        sysUser.UserId = Convert.ToInt32(claim.Value);
                    }
                    if(claim.Type.Equals("amr"))
                    {
                        sysUser.UserName = claim.Value;
                    }
                }
                bindingContext.Result = ModelBindingResult.Success(sysUser);
            }
            return Task.CompletedTask;
        }
    }
}
