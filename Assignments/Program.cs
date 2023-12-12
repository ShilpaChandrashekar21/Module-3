
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
    if(getAllPostResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        //JObject response = JObject.Parse(getAllPostResponse.Content);

        Console.WriteLine("Get All Post response: " + getAllPostResponse.Content);
    }
    else
    {
        Console.WriteLine(getAllPostResponse.ErrorMessage);
    }
   

}

static void GetOnePost(RestClient client)
{
    var getOnePostRequest = new RestRequest("posts/1", Method.Get);
    var getOnePostResponse = client.Execute(getOnePostRequest);
    if(getOnePostResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        JObject response = JObject.Parse(getOnePostResponse.Content);
        Console.WriteLine("Get One Post response: " + response);
    }
    else
    {
        Console.WriteLine(getOnePostResponse.ErrorMessage);
    }


}

static void GetAllComments(RestClient client)
{
    var getAllCommentsRequest = new RestRequest("posts/1/comments", Method.Get);
    var getAllCommentsResponse = client.Execute(getAllCommentsRequest);
    if(getAllCommentsResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        //JObject response = JObject.Parse(getAllCommentsResponse.Content);
        Console.WriteLine("Get All Comment response: " + getAllCommentsResponse.Content);

    }
    else
    {
        Console.WriteLine(getAllCommentsResponse.ErrorMessage);
    }
   
}

static void GetCommentsforOnePost(RestClient client)
{
    var getOneCommentRequest = new RestRequest("comments", Method.Get);
    getOneCommentRequest.AddQueryParameter("postId", "1");
    var getOneCommentResponse = client.Execute(getOneCommentRequest);
    if(getOneCommentResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        //JObject response = JObject.Parse(getOneCommentResponse.Content);
        Console.WriteLine("Get Comment for One Post response: " + getOneCommentResponse.Content);
    }
    else
    {
        Console.WriteLine(getOneCommentResponse.ErrorMessage);
    }
   

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
    if(createPostResponse.StatusCode == System.Net.HttpStatusCode.Created) 
    {
        JObject response = JObject.Parse(createPostResponse.Content);
        Console.WriteLine("Create Post response: " + response);
    }
    else
    {
        Console.WriteLine(createPostResponse.ErrorMessage);
    }
   
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
    if(updatePostResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        JObject response = JObject.Parse(updatePostResponse.Content);
        Console.WriteLine("Update Post response: " + response);
    }
    else
    {
        Console.WriteLine(updatePostResponse.ErrorMessage);
    }
   
}

static void DeletePost(RestClient client)
{
    var deletePostRequest = new RestRequest("posts/1", Method.Delete);
    var deletePostResponse = client.Execute(deletePostRequest);
    if(deletePostResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        Console.WriteLine("Delete Post response: " + deletePostResponse.Content);

    }
    else
    {
        Console.WriteLine(deletePostResponse.ErrorMessage);
    }
}

