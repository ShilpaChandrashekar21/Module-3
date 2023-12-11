
using Newtonsoft.Json.Linq;
using RestSharp;

string baseUrl = "https://reqres.in/api/";
var client = new RestClient(baseUrl);

//get
var getUserRequest = new RestRequest("users/2", Method.Get);

var getUserResponse = client.Execute(getUserRequest);
Console.WriteLine("\nGet response");
Console.WriteLine(getUserResponse.Content);

//post
var createUserRequest = new RestRequest("users", Method.Post);
createUserRequest.AddParameter("name", "John Doe");
createUserRequest.AddParameter("job", "Developer");

var createUserResponse = client.Execute(createUserRequest);
Console.WriteLine("\nPost response");
Console.WriteLine(createUserResponse.Content);

//put
var updateUserRequest = new RestRequest("users/2", Method.Put);
updateUserRequest.AddParameter("name", "Johnny");
updateUserRequest.AddParameter("job", "Artist");

var updateUserResponse = client.Execute(updateUserRequest);
Console.WriteLine("\nPut response");
Console.WriteLine(updateUserResponse.Content);

//delete

var deleteUserRequest = new RestRequest("users/2", Method.Delete);

var deleteUserResponse = client.Execute(deleteUserRequest);
Console.WriteLine("\nDelete response");
Console.WriteLine(deleteUserResponse.ResponseStatus);


var getUsersRequest = new RestRequest("users/2", Method.Get);
var getUsersResponse = client.Execute(getUsersRequest);
//response in json format

if (getUsersResponse.StatusCode == System.Net.HttpStatusCode.OK)
{
    JObject? userJson = JObject.Parse(getUsersResponse.Content);
    // string? pageno = userJson["page"]?.ToString();
    string? userFirstName = userJson["data"]?["first_name"]?.ToString();
    string? userLastName = userJson["data"]?["last_name"]?.ToString();
    Console.WriteLine(userJson);
    Console.WriteLine($"FirstName - {userFirstName},LastName - {userLastName}");

}
else
{
    Console.WriteLine($"Error - {getUsersResponse.ErrorMessage}");
}
/*Console.WriteLine("\nGet response for page 1");
Console.WriteLine(getUsersResponse.Content);
*/

//get with query parameter

GetAllUsers(client);
GetSingleUser(client);
CreateUser(client);
UpdateUser(client);

static void GetAllUsers(RestClient client)
{
    var getUsersRequest = new RestRequest("users", Method.Get);
    getUsersRequest.AddQueryParameter("page", "1");
    getUsersRequest.AddHeader("Content-Type", "application/json");
    var getUsersResponse = client.Execute(getUsersRequest);
    JObject? response = JObject.Parse(getUsersResponse.Content);

    Console.WriteLine("\n method Get response for page 1");
    Console.WriteLine(response);
}

static void GetSingleUser(RestClient client)
{
    var getUsersRequest = new RestRequest("users/2", Method.Get);

    var getUsersResponse = client.Execute(getUsersRequest);
    JObject response = JObject.Parse(getUsersResponse.Content);
    Console.WriteLine("\n method Get response");
    Console.WriteLine(response);
}

static void CreateUser(RestClient client)
{
    var createUsersRequest = new RestRequest("users", Method.Post);
    createUsersRequest.AddHeader("Content-Type", "application/json");
    createUsersRequest.AddJsonBody(new { name = "John-Doe", job = "Developer" });

    var createUsersResponse = client.Execute(createUsersRequest);
    Console.WriteLine("\n method Post response for response body");
    Console.WriteLine(createUsersResponse.Content);

}
//post with request body
var createUsersRequest = new RestRequest("users", Method.Post);
createUsersRequest.AddHeader("Content-Type", "application/json");
createUsersRequest.AddJsonBody(new { name = "John-Doe", job = "Developer" });


var createUsersResponse = client.Execute(createUsersRequest);
Console.WriteLine("\nPost response for response body");
Console.WriteLine(createUsersResponse.Content);

//put with response body
static void UpdateUser(RestClient client)
{
    var updateUsersRequest = new RestRequest("users/2", Method.Put);
    updateUsersRequest.AddHeader("Content-Type", "application/json");
    updateUsersRequest.AddJsonBody(new { name = "Johnny", job = "Artist" });

    var updateUsersResponse = client.Execute(updateUsersRequest);
    Console.WriteLine("\n method Put with response body");
    Console.WriteLine(updateUsersResponse.Content);

}

var updateUsersRequest = new RestRequest("users/2", Method.Put);
updateUsersRequest.AddHeader("Content-Type", "application/json");
updateUsersRequest.AddJsonBody(new { name = "Johnny", job = "Artist" });

var updateUsersResponse = client.Execute(updateUsersRequest);
Console.WriteLine("\n method Put with response body");
Console.WriteLine(updateUsersResponse.Content);


