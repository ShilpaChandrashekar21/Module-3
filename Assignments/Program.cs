
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Runtime.InteropServices.JavaScript;

string baseUrl = "https://jsonplaceholder.typicode.com/";
var client = new RestClient(baseUrl);

GetAllPost(client);
GetOnePost(client);
GetAllComments(client);
GetCommentsforOnePost(client);
CreatePost(client);
UpdatePost(client);
DeletePost(client);

static void GetAllPost(RestClient client)
{
    var getAllPostRequest = new RestRequest("posts", Method.Get);
    var getAllPostResponse = client.Execute(getAllPostRequest);
    //JObject response = JObject.Parse(getAllPostResponse.Content);
    Console.WriteLine("Get All Post response: " + getAllPostResponse.Content);

}

static void GetOnePost(RestClient client)
{
    var getOnePostRequest = new RestRequest("posts/1", Method.Get);
    var getOnePostResponse = client.Execute(getOnePostRequest);

    JObject response = JObject.Parse(getOnePostResponse.Content);
    Console.WriteLine("Get One Post response: " + response);
}

static void GetAllComments(RestClient client)
{
    var getAllCommentsRequest = new RestRequest("posts/1/comments", Method.Get);
    var getAllCommentsResponse = client.Execute(getAllCommentsRequest);

    //JObject response = JObject.Parse(getAllCommentsResponse.Content);
    Console.WriteLine("Get All Comment response: " + getAllCommentsResponse.Content);
}

static void GetCommentsforOnePost(RestClient client)
{
    var getOneCommentRequest = new RestRequest("comments", Method.Get);
    getOneCommentRequest.AddQueryParameter("postId", "1");
    var getOneCommentResponse = client.Execute(getOneCommentRequest);

    JObject response = JObject.Parse(getOneCommentResponse.Content);
    Console.WriteLine("Get Comment for One Post response: " + response);

}

static void CreatePost(RestClient client)
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
    JObject response = JObject.Parse(createPostResponse.Content);
    Console.WriteLine("Create Post response: " + response);
}

static void UpdatePost(RestClient client)
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
    JObject response = JObject.Parse(updatePostResponse.Content);
    Console.WriteLine("Update Post response: " + response);
}

static void DeletePost(RestClient client)
{
    var deletePostRequest = new RestRequest("posts/1", Method.Delete);
    var deletePostResponse = client.Execute(deletePostRequest);
    Console.WriteLine("Delete Post response: " + deletePostResponse.Content);
}

