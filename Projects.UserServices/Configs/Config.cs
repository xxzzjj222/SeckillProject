﻿using IdentityServer4.Models;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using System.Security.Cryptography;
using IdentityServer4.Test;

namespace Projects.UserServices.Configs
{
    public class Config
    {
        /// <summary>
        /// 资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("TeamService","TeamService")
            };
        }

        /// <summary>
        /// 客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId="client-password",
                    //客户端有权限的范围，openId必须添加，否则报错forbidden
                    AllowedScopes={"TeamService",IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile },
                    //使用密码进行验证
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    //用于认证的密码
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                },

                //openId
                new Client
                {
                    ClientId="client-code",
                    ClientSecrets={new Secret("secret".Sha256()) },
                    AllowedGrantTypes=GrantTypes.Code,
                    AllowedScopes={"TeamService",IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile },
                    RequireConsent=false,
                    RequirePkce=true,

                    RedirectUris={ "https://localhost:5006/signin-oidc" },//客户端地址
                    PostLogoutRedirectUris={ "https://localhost:5006/signout-callback-oidc"},// 2、登录退出地址

                    AllowOfflineAccess=true
                }
            };
        }

        /// <summary>
        /// //openId身份资源
        /// </summary>
        public static IEnumerable<IdentityResource> Ids => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        /// <summary>
        /// 测试用户
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="222222",
                    Username="333333",
                    Password="444444"
                }
            };
        }
    }
}
