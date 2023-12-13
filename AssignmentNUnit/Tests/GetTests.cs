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
    public class GetTests : CoreCodes
    {
        [Test]
        [Order(1)]
        public void GetAllPost()
        {

            test = extent.CreateTest("Get All Posts");
            Log.Information("Getting all posts");

            var getAllPostRequest = new RestRequest("posts", Method.Get);

            var getAllPostResponse = client.Execute(getAllPostRequest);
            try
            {
                Assert.That(getAllPostResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK));
                Log.Information("Response status code is as expected");
                Assert.IsNotEmpty(getAllPostResponse.Content);
                Log.Information("post response is returned");
                Log.Information($"Response Content {getAllPostResponse.Content}");
                test.Pass("Get all post test - passed");

            }
            catch(AssertionException ex)
            {
                test.Fail("Get all post test - failed "+ex.Message);
            }

            

        }

        [Test]
        [TestCase(1)]
        [Order(2)]

        public void GetOnePost(int id)
        {
            test = extent.CreateTest("Get Single Posts");
            Log.Information("Getting Single posts");

            var getOnePostRequest = new RestRequest("posts/"+id, Method.Get);
            var getOnePostResponse = client.Execute(getOnePostRequest);
            try
            {
                Assert.That(getOnePostResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK));
                Log.Information("Response status code is as expected");

                var response = JsonConvert.DeserializeObject<PostData>(getOnePostResponse.Content);
                Log.Information($"Response: {response}");

                Assert.IsNotNull(response);
                Log.Information("response is returned");

                Assert.IsNotEmpty(response.Body);
                Log.Information("post body is not empty");
                test.Pass("Get single post - passed");
            }
            catch(AssertionException ex)
            {
                test.Fail("Get single post - failed" + ex.Message);
            }
           


        }

        [Test]
        [TestCase(2)]
        [Order(3)]
        public void GetAllComments(int id)
        {
            test = extent.CreateTest("Get All Comments for single Post");
            Log.Information("Getting All Comments for single Post");

            var getAllCommentsRequest = new RestRequest("posts/"+id+"/comments", Method.Get);
            var getAllCommentsResponse = client.Execute(getAllCommentsRequest);
            try
            {
                Assert.That(getAllCommentsResponse.StatusCode == System.Net.HttpStatusCode.OK);
                Log.Information("Response status code is as expected");

                Assert.IsNotEmpty(getAllCommentsResponse.Content);

                Log.Information("response is returned" + getAllCommentsResponse.Content);

                test.Pass("Get All Comments for single Post - passed");
            }
            catch(AssertionException ex)
            {
                test.Fail("Get All Comments for single Post - failed"+ ex.Message);
            }




        }

        [Test]
        [TestCase(2)]
        [Order(4)]
        public void GetAllCommentsForOnePost(int queryParam)
        {
            test = extent.CreateTest("Get All Comments for single Post");
            Log.Information("Getting All Comments for single Post");

            var getOneCommentRequest = new RestRequest("comments", Method.Get);
            getOneCommentRequest.AddQueryParameter("postId", queryParam);
            var getOneCommentResponse = client.Execute(getOneCommentRequest);
            try
            {
                Assert.That(getOneCommentResponse.StatusCode == System.Net.HttpStatusCode.OK);

                List<CommentData> commentData = JsonConvert.DeserializeObject<List<CommentData>>(getOneCommentResponse.Content);

                Assert.IsNotNull(commentData);
                Assert.That(commentData[0].PostId.Equals(queryParam));

                test.Pass("Get All Comments for single Post -passed");
            }
            catch (AssertionException ex)
            {
                test.Fail("Get All Comments for single Post - failed" + ex.Message);
            }
           
        }

    }
}
