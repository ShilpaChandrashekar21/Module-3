using AventStack.ExtentReports;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports.Reporter;

using Serilog;
using RestSharpNUnit.Utilities;

namespace RestSharpNUnit
{
    [TestFixture]
    public class ReqResTests : CoreCodes
    {
        
        [Test]
        [Order(1)]
        public void GetAllUser()
        {
            test = extent.CreateTest("Get All User");
            Log.Information("Getting all the users");
            var getUsersRequest = new RestRequest("users", Method.Get);
            getUsersRequest.AddQueryParameter("page", "1");
            try
            {
               
                getUsersRequest.AddHeader("Content-Type", "application/json");
                var getUsersResponse = client.Execute(getUsersRequest);
                Assert.That(getUsersResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK)); ;
                Log.Information("Response status code returned as expected");

                JObject? response = JObject.Parse(getUsersResponse.Content);

                Assert.IsNotNull(response);
                Log.Information("All the user data is returned");
                Log.Information("Get All Users Test all asserts passed");

                test.Pass("Get All users test passed");

            }
            catch(AssertionException ex)
            {
                test.Fail("Get All users test failed"+ ex.Message);
            }
            
        }

        [Test]
        [Order(2)]
        public void GetSingleUser()
        {
            test = extent.CreateTest("Get single User");
            Log.Information("Getting all the users");

            var getUsersRequest = new RestRequest("users/2", Method.Get);
            var getUsersResponse = client?.Execute(getUsersRequest);
            try
            {
              
                Assert.That(getUsersResponse?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("Response status code returned as expected");

                var response = JsonConvert.DeserializeObject<UserDataResponse>(getUsersResponse?.Content);
                UserData? userData = response?.Data;

                Assert.NotNull(userData);
                Log.Information("user data is returned");
                Assert.That(userData.Id.Equals(2));
                Log.Information("The Id of user matches to the expected result");
                Assert.IsNotEmpty(userData.FirstName);
                Log.Information("user FirstNAme is not null");

                Log.Information("Get Single User Test all asserts passed");
                test.Pass("Get single user test passed");

            }
            catch(AssertionException ex)
            {
                test.Fail("Get single user test failed"+ex.Message);
            }



        }

        [Test]
        [Order(3)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create user");
            Log.Information("Creating the user");

            var createUsersRequest = new RestRequest("users", Method.Post);
            createUsersRequest.AddHeader("Content-Type", "application/json");
            createUsersRequest.AddJsonBody(new
            {
                name = "John-Doe",
                job = "Developer"
            });

            try
            {
                
                var createUsersResponse = client.Execute(createUsersRequest);
                Assert.That(createUsersResponse?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information("Response status code returned as expected");

                var user = JsonConvert.DeserializeObject<UserData>(createUsersResponse?.Content);
                Assert.IsNotNull(user);
                Log.Information("user data created & returned");

                Assert.That(user.Job.Equals("Developer"));
                Log.Information("The Job of user matches to the expected result");
                Log.Information("Create user Test all asserts passed");
                test.Pass("Create user test passed");

            }
            catch (AssertionException ex)
            {
                test.Fail("Create user test failed" + ex.Message);
            }



        }

        [Test]
        [Order(4)]
        public void UpdateUser()
        {
            test = extent.CreateTest("Update user");
            Log.Information("Updating the user");

            var updateUsersRequest = new RestRequest("users/2", Method.Put);
            updateUsersRequest.AddHeader("Content-Type", "application/json");
            updateUsersRequest.AddJsonBody(new
            {
                name = "Johnny",
                job = "Artist"
            });

            var updateUsersResponse = client.Execute(updateUsersRequest);
            try
            {
               
                Assert.That(updateUsersResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK));
                Log.Information("Response status code returned as expected");

                var user = JsonConvert.DeserializeObject<UserData>(updateUsersResponse.Content);

                Assert.IsNotNull(user);
                Log.Information("user data updated & returned");

                Assert.That(user.Job.Equals("Artist"));
                Log.Information("The Job of user matches to the expected result");
                Log.Information("Update user Test all asserts passed");
                test.Pass("Update user test passed");

            }
            catch (AssertionException ex)
            {
                test.Fail("Update user test failed" + ex.Message);
            }


        }

        [Test]
        [Order(5)]
        public void DeleteUser()
        {
            test = extent.CreateTest("Delete user");
            Log.Information("Deleting the user");

            var deleteUserRequest = new RestRequest("users/2", Method.Delete);

            var deleteUserResponse = client.Execute(deleteUserRequest);
            try
            {
               

                Assert.That(deleteUserResponse.StatusCode.Equals(System.Net.HttpStatusCode.NoContent));
                Log.Information("Response status code returned as expected");

                Assert.IsEmpty(deleteUserResponse.Content);
                Log.Information("user data deleted & returned");
                Log.Information("Delete user Test all asserts passed");
                test.Pass("Delete user test passed");

            }
            catch (AssertionException ex)
            {
                test.Fail("Delete user test failed" + ex.Message);
            }


        }

        [Test]
        [Order(6)]
        public void GetNonExistingUser()
        {
            test = extent.CreateTest("Get Non Existing user");
            Log.Information("Getting the non existing user");

            var req = new RestRequest("users/888", Method.Get);
            var resp = client.Execute(req);
            try
            {
                

                Assert.That(resp.StatusCode.Equals(System.Net.HttpStatusCode.NotFound));
                Log.Information("Response status code returned as expected");
                Log.Information("Getting non existing user Test all asserts passed");
                test.Pass("Getting non existing user test passed");

            }
            catch (AssertionException ex)
            {
                test.Fail("Getting non existing  user test failed" + ex.Message);
            }


        }
    }
}
