using AssignmentNUnit.Utilities;
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
    public class DeleteTests : CoreCodes
    {

        [Test]
        [TestCase(1)]
        [Order(7)]

        public void DeletePost(int id)
        {
            test = extent.CreateTest(" Delete Post");
            Log.Information("Delete post");
            var deletePostRequest = new RestRequest("posts/"+id, Method.Delete);
            var deletePostResponse = client.Execute(deletePostRequest);
            try
            {
                Assert.That(deletePostResponse.StatusCode == System.Net.HttpStatusCode.OK);
                Log.Information("Response code is returned as expected");

                Assert.IsNotNull(deletePostResponse.Content);
                Log.Information("Deleted and returned response" + deletePostResponse.Content);
                test.Fail("Delete Post Test - passed");
            }
            catch (AssertionException ex)
            {
                test.Fail("Delete Post Test - failed " + ex.Message);
            }




        }
    }
}
