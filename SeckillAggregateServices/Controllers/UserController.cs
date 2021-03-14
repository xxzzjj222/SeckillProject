using AutoMapper;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projects.Common.Exceptions;
using Projects.Cores.DynamicMiddleware.Urls;
using SeckillAggregateServices.Dtos.UserService;
using SeckillAggregateServices.Models.UserService;
using SeckillAggregateServices.Pos.UserService;
using SeckillAggregateServices.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserClient _userClient;
        private readonly IDynamicMiddleUrl _dynamicMiddleUrl;
        private readonly HttpClient _httpClient;

        public UserController(IUserClient userClient,
            IDynamicMiddleUrl dynamicMiddleUrl,
            IHttpClientFactory httpClientFactory)
        {
            Console.WriteLine("UserController Constructor");
            _userClient = userClient;
            _dynamicMiddleUrl = dynamicMiddleUrl;
            _httpClient = httpClientFactory.CreateClient();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]
        public UserDto PostLogin([FromForm]LoginPo loginPo)
        {
            //1.查询用户信息
            //2.判断用户信息是否存在
            //3.将用户信息生成token进行存储
            //4.将token信息存储到cookie或session
            //5.返回成功信息和token
            //6.对token进行认证

            //1.获取identityServer接口文档
            string userUrl = _dynamicMiddleUrl.GetMiddleUrl("https", "UserServices");
            DiscoveryDocumentResponse discoveryDocument = _httpClient.GetDiscoveryDocumentAsync(userUrl).Result;
            if(discoveryDocument.IsError)
            {
                Console.WriteLine($"[DiscoveryDocumentResponse Error]: {discoveryDocument.Error}");
            }

            //2.根据用户名和密码建立token
            TokenResponse tokenResponse = _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client-password",
                ClientSecret = "secret",
                GrantType = "password",
                UserName = loginPo.UserName,
                Password = loginPo.Password
            }).Result;

            //3.返回AccessToken
            if (tokenResponse.IsError)
            {
                throw new BizException(tokenResponse.Error + "," + tokenResponse.Raw);
            }

            //4.获取用户信息
            UserInfoResponse userInfoResponse = _httpClient.GetUserInfoAsync(new UserInfoRequest()
            {
                Address = discoveryDocument.UserInfoEndpoint,
                Token = tokenResponse.AccessToken
            }).Result;

            //5.返回UserDto信息
            UserDto userDto = new UserDto();
            userDto.UserId = userInfoResponse.Json.TryGetString("sub");
            userDto.UserName = loginPo.UserName;
            userDto.AccessToken = tokenResponse.AccessToken;
            userDto.ExpiresIn = tokenResponse.ExpiresIn;

            // 1、加密方式有很多，证书加密，数据安全的

            // 1、cookie存储
            // 2、本地缓存

            return userDto;
        }

        public void RefreshToken()
        {

        }

        [HttpPost]
        public User Post([FromForm] UserPo userPo)
        {
            Console.WriteLine("UserController Post");
            var configuration = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<UserPo, User>();
              });

            IMapper mapper = configuration.CreateMapper();

            User user = mapper.Map<User>(userPo);
            user.CreateTime = new DateTime();

            user = _userClient.RegistryUsers(user);

            return user;
        }

    }
}
