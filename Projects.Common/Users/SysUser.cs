using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Common.Users
{
    /// <summary>
    /// 系统用户，用于存储用户状态相关信息
    /// </summary>
    public class SysUser
    {
        public int UserId { set; get; } // 用户Id
        public string UserName { set; get; } // 用户名称
    }
}
