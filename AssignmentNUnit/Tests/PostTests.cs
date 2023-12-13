using AssignmentNUnit.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNUnit.Tests
{
    [TestFixture]
    public class PostTests : CoreCodes
    {
        [Test]
        [Order(5)]

        public void CreatePost()
        {
            test = extent.CreateTest("Create Post");
            Log.Information("Creating new Post");
            var createPostRequest = new RestRequest("posts", Method.Post);
            createPostRequest.AddHeader("Content-Type", "application/json");
            createPostRequest.AddJsonBody(new
            {
                userId = 1,
                id = 1,
                title = "comics",
                body = "empty"
            });

            var createPostResponse = client.Execute(createPostRequest);
            try
            {
                Assert.That(createPostResponse.StatusCode.Equals(System.Net.HttpStatusCode.Created));
                Log.Information("Response code is as expected");
                var response = JsonConvert.DeserializeObject<PostData>(createPostResponse.Content);

                Assert.IsNotNull(response);
                Log.Information("post created and returned response" + response);
                Assert.That(response.Title.Equals("comics"));
                Log.Information("Checked for post title");

                Log.Information("All asserts for create post test - passed");

                test.Pass("create post test - passed");
            }
            catch(AssertionException ex)
            {
                test.Fail("create post test - failed "+ ex.Message);
            }

            

        }
    }
}
