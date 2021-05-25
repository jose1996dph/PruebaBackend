using Domain.Models;
using Domain.Responses.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using StingTest;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TestStingTest.Helpers;
using TestStingTest.Models;

namespace TestStingTest
{
    public class TaskTest
    {
        private TestServer _server;
        private HttpClient _client;

        int userId;
        string email;
        LoginReponse token;

        [SetUp]
        public void Setup()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();


            var random = new Random();
            var userHelper = new UserHelper();
            this.email = "email" + random.Next().ToString() + "@email.com";
            var user = userHelper.CreateUser(_client, new
            {
                Email = this.email,
                Full_Name = "User",
                Password = "Password",
                Confirm_Password = "Password",
                Photo = Constants.PHOTO,
                Photo_Name = "photo.png",
            }).Result;
            userId = user.UserId;

            var authHelper = new AuthHelper();

            var token = authHelper.Login(_client, this.email).Result;

            this.token = token;
        }

        [Test]
        public void CreateTaskTest()
        {
            var taskRequest = new
            {
                title = "title",
                descriptin = "descriptin",
                start_date = "2021-5-25",
                due_date = "2021-5-25",
                user_id = userId,
                priority = 1
            };

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var content = JsonConvert.SerializeObject
            (
                taskRequest,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            using var param = new StringContent(content, Encoding.UTF8, "application/json");

            using var response = _client.PostAsync("/api/Tasks/", param).Result;

            var result = response.Content.ReadAsStringAsync().Result;

            var task = JsonConvert.DeserializeObject<Task>
            (
                result,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            Assert.True(task != null);
        }

        [Test]
        public void ShowTasksTest()
        {
            var taskHelper = new TaskHelper();

            var task = taskHelper.CreateTask(_client, token.AccessToken, userId).Result;

            var authHelper = new AuthHelper();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            using var response = _client.GetAsync("/api/Tasks").Result;

            var result = response.Content.ReadAsStringAsync().Result;

            var Tasks = JsonConvert.DeserializeObject<List<Task>>
            (
                result,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            Assert.True(Tasks.Count > 0);
        }

        [Test]
        public void UpdateTaskTest()
        {
            var taskHelper = new TaskHelper();

            var task = taskHelper.CreateTask(_client, token.AccessToken, userId).Result;

            var taskRequest = new
            {
                title = "title",
                description = "descriptin",
                start_date = DateTime.Today,
                due_date = DateTime.Today,
                user_id = task.UserId,
                priority = 1
            };

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var content = JsonConvert.SerializeObject
            (
                taskRequest,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            using var param = new StringContent(content, Encoding.UTF8, "application/json");

            using var response = _client.PutAsync("/api/Tasks/" + task.TaskId, param).Result;

            var result = response.Content.ReadAsStringAsync().Result;

            Assert.True(response.IsSuccessStatusCode);
        }

        [Test]
        public void ShowTaskTest()
        {
            var taskHelper = new TaskHelper();

            var task = taskHelper.CreateTask(_client, token.AccessToken, userId).Result;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            using var response = _client.GetAsync("/api/Tasks/" + task.TaskId).Result;

            var result = response.Content.ReadAsStringAsync().Result;

            var TaskResponse = JsonConvert.DeserializeObject<Task>
            (
                result,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            Assert.True(TaskResponse.TaskId == task.TaskId);
        }

        [Test]
        public void DeleteTaskTest()
        {
            var taskHelper = new TaskHelper();

            var task = taskHelper.CreateTask(_client, token.AccessToken, userId).Result;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            using var response = _client.DeleteAsync("/api/Tasks/" + task.TaskId).Result;

            var result = response.Content.ReadAsStringAsync().Result;

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
