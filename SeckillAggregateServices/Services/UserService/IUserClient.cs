using Projects.Cores.MicroClient.Attributes;
using SeckillAggregateServices.Models.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Services.UserService
{
    /// <summary>
    /// 用户微服务客户端
    /// </summary>
    [MicroClient("https","UserServices")]
    public interface IUserClient
    {
        [PostPath("/Users")]
        public User RegistryUsers(User user);
    }
}
