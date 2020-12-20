using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Pos.UserService
{
    /// <summary>
    /// 用户参数
    /// </summary>
    public class UserPo
    {
        public string UserName { set; get; } // 用户名
        public string Password { set; get; }// 密码
        public string UserQQ { set; get; } // 用户QQ
        public string UserPhone { set; get; }// 用户手机号
    }
}
