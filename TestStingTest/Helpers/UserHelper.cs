using Domain.Requests.User;
using Domain.Responses;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestStingTest.Models;

namespace TestStingTest.Helpers
{
    class UserHelper
    {
        public async Task<UserResult> CreateUser(HttpClient _client, object userRequest = null)
        {
            if (userRequest == null)
            {
                userRequest = new
                {
                    Email = "Email@email.email",
                    Full_Name = "User",
                    Password = "Password",
                    Confirm_Password = "Password",
                    Photo = Constants.PHOTO,
                    Photo_Name = "photo.png",
                };
            }

            var content = JsonConvert.SerializeObject
            (
                userRequest,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            using var param = new StringContent(content, Encoding.UTF8, "application/json");

            using var response = await _client.PostAsync("/api/users", param);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserResult>
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
