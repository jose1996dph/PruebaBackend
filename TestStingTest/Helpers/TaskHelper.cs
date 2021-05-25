using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TestStingTest.Models;

namespace TestStingTest.Helpers
{
    class TaskHelper
    {
        public async System.Threading.Tasks.Task<Task> CreateTask(HttpClient _client, string AccessToken, int userId)
        {
            var taskRequest = new
            {
                title = "title",
                description = "descriptin",
                due_date = DateTime.Now,
                start_date = DateTime.Now,
                user_id = userId,
                priority = 1
            };

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            var content = JsonConvert.SerializeObject
            (
                taskRequest,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            using var param = new StringContent(content, Encoding.UTF8, "application/json");

            using var response = await _client.PostAsync("/api/Tasks/", param);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Task>
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
