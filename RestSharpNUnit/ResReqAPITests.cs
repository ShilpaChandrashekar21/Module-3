using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpNUnit
{
    [TestFixture]
    internal class ResReqAPITests
    {
        private RestClient? client;
        private string? baseUrl = "https://reqres.in/api/";

        [SetUp]
        public void Initialization()
        {
            client = new RestClient(baseUrl);
        }

        [Test]
        public void GetAllUser()
        {
            var getUsersRequest = new RestRequest("users", Method.Get);
            getUsersRequest.AddQueryParameter("page", "1");

            getUsersRequest.AddHeader("Content-Type", "application/json");
            var getUsersResponse = client.Execute(getUsersRequest);

            JObject? response = JObject.Parse(getUsersResponse.Content);

           Assert.IsNotNull(response);
        }

        [Test]
        public void GetSingleUser()
        {
            var getUsersRequest = new RestRequest("users/2", Method.Get);
            var getUsersResponse = client?.Execute(getUsersRequest);
           

            Assert.That(getUsersResponse?.StatusCode,Is.EqualTo(System.Net.HttpStatusCode.OK));
            
            var response = JsonConvert.DeserializeObject<UserDataResponse>(getUsersResponse?.Content);
            UserData? userData = response?.Data;
            
            Assert.NotNull(userData);
            Assert.That(userData.Id.Equals(2));
            Assert.IsNotEmpty(userData.Email);
        }

        [Test]
        public void CreateUser()
        {
            var createUsersRequest = new RestRequest("users", Method.Post);
            createUsersRequest.AddHeader("Content-Type", "application/json");
            createUsersRequest.AddJsonBody(new 
            { name = "John-Doe",
              job = "Developer" 
            });

            var createUsersResponse = client.Execute(createUsersRequest);
            Assert.That(createUsersResponse?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            
            var user = JsonConvert.DeserializeObject<UserData>(createUsersResponse?.Content);   
            Assert.IsNotNull(user);
           // Assert.That(user.FirstName.Equals("John"));

        }

        [Test]
        public void UpdateUser()
        {
            var updateUsersRequest = new RestRequest("users/2", Method.Put);
            updateUsersRequest.AddHeader("Content-Type", "application/json");
            updateUsersRequest.AddJsonBody(new 
            { name = "Johnny", 
              job = "Artist" 
            });

            var updateUsersResponse = client.Execute(updateUsersRequest);
            Assert.That(updateUsersResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK));

            var user = JsonConvert.DeserializeObject<UserData>(updateUsersResponse.Content);

            Assert.IsNotNull(user);
            Assert.That(user.Job.Equals("Artist"));

        }

        [Test]
        public void DeleteUser()
        {
            var deleteUserRequest = new RestRequest("users/2", Method.Delete);

            var deleteUserResponse = client.Execute(deleteUserRequest);

            Assert.That(deleteUserResponse.StatusCode.Equals(System.Net.HttpStatusCode.NoContent));
            
            Assert.IsEmpty(deleteUserResponse.Content);

        }

        [Test]
        public void GetNonExistingUser()
        {
            var req = new RestRequest("users/888",Method.Get);
            var resp = client.Execute(req);

            Assert.That(resp.StatusCode.Equals(System.Net.HttpStatusCode.NotFound));
        }
    }
}
