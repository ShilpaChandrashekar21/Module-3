using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNUnit
{
    [TestFixture]
    public class JsonplaceholderAPITests
    {
        private RestClient client;
        private string? baseUrl = "https://jsonplaceholder.typicode.com/";

        [OneTimeSetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }

        [Test]
        public void GetAllPost()
        {
            var getAllPostRequest = new RestRequest("posts", Method.Get);

            var getAllPostResponse = client.Execute(getAllPostRequest);

            Assert.That(getAllPostResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK));
           
            Assert.IsNotEmpty(getAllPostResponse.Content);
            Console.WriteLine(getAllPostResponse.Content);
        
        }

        [Test]

        public void GetOnePost()
        {
            var getOnePostRequest = new RestRequest("posts/1", Method.Get);
            var getOnePostResponse = client.Execute(getOnePostRequest);
            Assert.That(getOnePostResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK));
           
            var response = JsonConvert.DeserializeObject<PostData>(getOnePostResponse.Content);
            Console.WriteLine($"Response: {response}");
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.Body);
           
       
        }

        [Test]
        public void GetAllComments()
        {
            var getAllCommentsRequest = new RestRequest("posts/1/comments", Method.Get);
            var getAllCommentsResponse = client.Execute(getAllCommentsRequest);

            Assert.That(getAllCommentsResponse.StatusCode == System.Net.HttpStatusCode.OK);
            
             Assert.IsNotEmpty(getAllCommentsResponse.Content);

        }

        [Test]
        public void GetCommentsForOnePost()
        {
            var getOneCommentRequest = new RestRequest("comments", Method.Get);
            getOneCommentRequest.AddQueryParameter("postId", "1");
            var getOneCommentResponse = client.Execute(getOneCommentRequest);
            Assert.That(getOneCommentResponse.StatusCode == System.Net.HttpStatusCode.OK);

            List<CommentData> commentData = JsonConvert.DeserializeObject<List<CommentData>>(getOneCommentResponse.Content);

            Assert.IsNotNull (commentData);
            Assert.That(commentData[0].PostId, Is.EqualTo(1));
        }

        [Test]

        public void CreatePost( )
        {
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

            Assert.That(createPostResponse.StatusCode.Equals(System.Net.HttpStatusCode.Created));
            
            var  response = JsonConvert.DeserializeObject<PostData>(createPostResponse.Content);
            
            Assert.IsNotNull (response);
            Assert.That(response.Title.Equals("comics"));
           
        }

        [Test]
        public void UpdatePost()
        {
            var updatePostRequest = new RestRequest("posts/1", Method.Put);
            updatePostRequest.AddHeader("Content-Type", "application/json");
            updatePostRequest.AddJsonBody(new
            {
                userId = 2,
                id = 12,
                title = "comics",
                body = "empty"
            });
            var updatePostResponse = client.Execute(updatePostRequest);
            Assert.That(updatePostResponse.StatusCode == System.Net.HttpStatusCode.OK);
            var response = JsonConvert.DeserializeObject<PostData>(updatePostResponse.Content);

            Assert.IsNotNull(response);
            Assert.That(response.UserId.Equals(2));

        }

        [Test]

        public void DeletePost()
        {
            var deletePostRequest = new RestRequest("posts/1", Method.Delete);
            var deletePostResponse = client.Execute(deletePostRequest);
            Assert.That(deletePostResponse.StatusCode == System.Net.HttpStatusCode.OK);
             
            Assert.IsNotNull(deletePostResponse.Content);

           
        }


    }
}
