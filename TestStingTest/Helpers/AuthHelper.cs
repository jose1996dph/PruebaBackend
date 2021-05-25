using Domain.Requests.Auth;
using Domain.Responses;
using Domain.Responses.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestStingTest.Helpers
{
    class AuthHelper
    {
        public async Task<LoginReponse> Login(HttpClient _client, string username = null, string password = null)
        {
            var loginRequest = new LoginRequest
            {
                Username = username ?? "Email@email.email",
                Password = password ?? "Password",
            };

            var content = JsonConvert.SerializeObject
            (
                loginRequest,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            using var param = new StringContent(content, Encoding.UTF8, "application/json");

            using var response = await _client.PostAsync("/api/auth/login", param);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<LoginReponse>
            (
                result,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );
        }
    }
}
