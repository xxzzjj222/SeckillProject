using IdentityModel;
using IdentityServer4.Validation;
using Projects.Common.Exceptions;
using Projects.UserServices.Models;
using Projects.UserServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projects.UserServices.IdentityServer
{
    /// <summary>
    /// 自定义资源持有者验证
    /// 从数据库获取用户信息验证
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserService _userService;

        public ResourceOwnerPasswordValidator(IUserService userService)
        {
            _userService = userService;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //根据用户名获取用户
            User user = _userService.GetUser(context.UserName);

            //判断User
            if (user == null)
            {
                throw new BizException($"数据库用户不存在:{context.UserName}");
            }
            if (!context.Password.Equals(user.Password))
            {
                throw new BizException($"数据库用户密码不正确");
            }

            context.Result = new GrantValidationResult(
                subject: user.Id.ToString(),
                authenticationMethod: user.UserName,
                claims: GetUserClaims(user));

            await Task.CompletedTask;
        }

        public Claim[] GetUserClaims(User user)
        {
            return new Claim[]
            {
                new Claim(JwtClaimTypes.Subject,user.Id.ToString()??""),
                new Claim(JwtClaimTypes.Id,user.Id.ToString()??""),
                new Claim(JwtClaimTypes.Name,user.UserName??""),
                new Claim(JwtClaimTypes.PhoneNumber,user.UserPhone??"")
            };
        }
    }
}
