using Domain.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using StingTest;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TestStingTest.Helpers;

namespace TestStingTest
{
    [TestFixture]
    public class UserTest
    {
        private TestServer _server;
        private HttpClient _client;
        [SetUp]
        public void setup()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Test]
        public void CreateUserTest()
        {
            var userHelper = new UserHelper();

            var user = userHelper.CreateUser(_client).Result;

            Assert.True(user != null);
        }

        [Test]
        public void ShowUsersTest()
        {
            var userHelper = new UserHelper();

            var user = userHelper.CreateUser(_client).Result;

            var authHelper = new AuthHelper();

            var token = authHelper.Login(_client).Result;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            using var response = _client.GetAsync("/api/users").Result;

            var result = response.Content.ReadAsStringAsync().Result;

            var users = JsonConvert.DeserializeObject<List<UserResponse>>
            (
                result,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            Assert.True(users.Count > 0);
        }

        [Test]
        public void UpdateUSerTest()
        {
            var userHelper = new UserHelper();

            var userRequest = new
            {
                Email = "Emailupdate@email.email",
                Full_Name = "Username",
                Password = "Password",
                Confirm_Password = "Password",
                Photo = Constants.PHOTO,
                Photo_Name = "photo.png",
            };

            var user = userHelper.CreateUser(_client, userRequest).Result;

            var authHelper = new AuthHelper();

            var token = authHelper.Login(_client, "Emailupdate@email.email", "Password").Result;

            userRequest = new
            {
                Email = "Emailupdate@email.email",
                Full_Name = "User_name",
                Password = "Password",
                Confirm_Password = "Password",
                Photo = Constants.PHOTO,
                Photo_Name = "photo.png",
            };

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var content = JsonConvert.SerializeObject
            (
                userRequest,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            using var param = new StringContent(content, Encoding.UTF8, "application/json");

            using var response = _client.PutAsync("/api/users/" + user.UserId, param).Result;

            var result = response.Content.ReadAsStringAsync().Result;

            Assert.True(response.IsSuccessStatusCode);
        }

        [Test]
        public void ShowUserTest()
        {
            var userHelper = new UserHelper();

            var createUserRequest = new
            {
                Email = "Emailshow@email.email",
                Full_Name = "User_name",
                Password = "Password",
                Confirm_Password = "Password",
                Photo = Constants.PHOTO,
                Photo_Name = "photo.png",
            };

            var user = userHelper.CreateUser(_client, createUserRequest).Result;

            var authHelper = new AuthHelper();

            var token = authHelper.Login(_client, "Emailshow@email.email", "Password").Result;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            using var response = _client.GetAsync("/api/users/" + user.UserId).Result;

            var result = response.Content.ReadAsStringAsync().Result;

            var userResponse = JsonConvert.DeserializeObject<UserResponse>
            (
                result,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            Assert.True(userResponse.Email == user.Email);
        }

        [Test]
        public void DeleteUserTest()
        {
            var userHelper = new UserHelper();

            var createUserRequest = new
            {
                Email = "Emaildelete@email.email",
                Full_Name = "User_name",
                Password = "Password",
                Confirm_Password = "Password",
                Photo = Constants.PHOTO,
                Photo_Name = "photo.png",
            };

            var user = userHelper.CreateUser(_client, createUserRequest).Result;

            var authHelper = new AuthHelper();

            var token = authHelper.Login(_client, "Emaildelete@email.email", "Password").Result;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            using var response = _client.DeleteAsync("/api/users/" + user.UserId).Result;

            var result = response.Content.ReadAsStringAsync().Result;

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
