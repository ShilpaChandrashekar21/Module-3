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
    public class UpdateTests : CoreCodes
    {
        [Test]
        [TestCase(1)]
        [Order(6)]

        public void UpdatePost(int id)
        {
            test = extent.CreateTest(" Update Post");
            Log.Information("Updating post");
            var updatePostRequest = new RestRequest("posts/"+ id, Method.Put);
            updatePostRequest.AddHeader("Content-Type", "application/json");
            updatePostRequest.AddJsonBody(new
            {
                userId = 2,
                id = 12,
                title = "comics",
                body = "empty"
            });
            var updatePostResponse = client.Execute(updatePostRequest);
            try
            {
                Assert.That(updatePostResponse.StatusCode == System.Net.HttpStatusCode.OK);
                Log.Information("Response code is returned as expected");

                var response = JsonConvert.DeserializeObject<PostData>(updatePostResponse.Content);

                Assert.IsNotNull(response);
                Log.Information("Updated and returned response" + response);
                Assert.That(response.UserId.Equals(2));
                Log.Information("Checked for updated user Id");
                test.Fail("Update Post Test - passed");
            }
            catch (AssertionException ex)
            {
                test.Fail("Update Post Test - failed "+ex.Message);
            }
            

        }
    }
}
