using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Pos.UserService
{
    /// <summary>
    /// 登录表单
    /// </summary>
    public class LoginPo
    {
        public string UserName { set; get; } // 用户名
        public string Password { set; get; }// 密码 
    }
}
