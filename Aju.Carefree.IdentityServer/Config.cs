using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Aju.Carefree.IdentityServer
{
    public static class Config
    {
        /// <summary>
        /// 获取授权用户
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="1",
                    Username="alice",
                    Password="password"
                },
                new TestUser
                {
                    SubjectId="2",
                    Username="aju",
                    Password="password"
                }
            };
        }

        /// <summary>
        /// 获取受保护的身份
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResource()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        /// <summary>
        /// 获取受保护的Api
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api","My Api")
            };
        }

        /// <summary>
        /// 获取受保护的用户信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedScopes={"api1"}
                },
                new Client
                {
                    ClientId="ro.client",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedScopes={"api1"}
                }
            };
        }
    }
}
