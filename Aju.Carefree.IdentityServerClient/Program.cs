using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Aju.Carefree.IdentityServerClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:16250");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }
            #region 如果使用ResourceOwnerPassword 请取消下面的注释
            //var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            //{
            //    Address = disco.TokenEndpoint,
            //    ClientId = "client",
            //    ClientSecret = "secret",

            //    UserName = "aju",
            //    Password = "password",
            //    Scope = "api1"
            //});
            #endregion

            #region 使用ClientCredentials
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",

                Scope = "api1"
            });

            #endregion
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("http://localhost:26581/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
