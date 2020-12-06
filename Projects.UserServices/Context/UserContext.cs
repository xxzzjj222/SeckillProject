using Microsoft.EntityFrameworkCore;
using Projects.UserServices.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.UserServices.Context
{
    /// <summary>
    /// 用户服务上下文
    /// </summary>
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            :base(options)
        {

        }

        //用户集合
        public DbSet<User> Users { get; set; }
    }
}
