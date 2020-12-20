using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Dtos.UserService
{
    /// <summary>
    /// 用户登录成功返回的信息
    /// </summary>
    public class UserDto
    {
        public string AccessToken { set; get; } // 执行token(用户身份)
        public int ExpiresIn { set; get; }// AccessToken过期时间
        public string UserName { set; get; }// 用户名
        public string UserId { set; get; }//用户id
    }
}
