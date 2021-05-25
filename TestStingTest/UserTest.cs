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
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public UserTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        string token;
        string userId;

        [Test]
        public async void CreateUserTest()
        {
            var userHelper = new UserHelper();

            var user = await userHelper.CreateUser(_client);

            Assert.True(user != null);
        }

        [Test]
        public async void ShowUsersTest()
        {
            var userHelper = new UserHelper();

            var user = await userHelper.CreateUser(_client);

            var authHelper = new AuthHelper();

            var token = await authHelper.Login(_client);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            using var response = await _client.GetAsync("/api/users");

            var result = await response.Content.ReadAsStringAsync();

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
        public async void UpdateUSerTest()
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

            var user = await userHelper.CreateUser(_client, userRequest);

            var authHelper = new AuthHelper();

            var token = await authHelper.Login(_client, "Emailupdate@email.email", "Password");

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

            using var response = await _client.PutAsync("/api/users/" + user.UserId, param);

            var result = await response.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
        }

        [Test]
        public async void ShowUserTest()
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

            var user = await userHelper.CreateUser(_client, createUserRequest);

            var authHelper = new AuthHelper();

            var token = await authHelper.Login(_client, "Emailshow@email.email", "Password");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            using var response = await _client.GetAsync("/api/users/" + user.UserId);

            var result = await response.Content.ReadAsStringAsync();

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
        public async void DeleteUserTest()
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

            var user = await userHelper.CreateUser(_client, createUserRequest);

            var authHelper = new AuthHelper();

            var token = await authHelper.Login(_client, "Emaildelete@email.email", "Password");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            using var response = await _client.DeleteAsync("/api/users/" + user.UserId);

            var result = await response.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
