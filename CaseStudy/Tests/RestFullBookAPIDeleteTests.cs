using CaseStudy.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Tests
{
    public class RestFullBookAPIDeleteTests :CoreCodes
    {
        [Test]
        [Order(1)]
        [TestCase(20)]
        public void DeleteBooking(int id)
        {
            test = extent.CreateTest(" Delete Booking");
            Log.Information("Deleting Booking");
            var request = new RestRequest("auth", Method.Post);
            request.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });

            var response = client.Execute(request);

            var token = JsonConvert.DeserializeObject<HeaderData>(response.Content);

            var deleteRequest = new RestRequest("booking/" + id, Method.Delete);
            deleteRequest.AddHeader("Cookie", "token=" + token.Token);
            deleteRequest.AddHeader("Content-Type", "application/json");
            deleteRequest.AddHeader("Accept", "application/json");

            var deleteResponse = client.Execute(deleteRequest);

            try
            {
                Assert.That(deleteResponse.StatusCode,Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information("response is as expected");
                Assert.That(deleteResponse.Content.Equals("Created"));
                Log.Information("response returned" + deleteResponse.Content);

                test.Pass("Delete Booking test - passed");

                
            }
            catch(AssertionException)
            {
                test.Fail("Delete Booking test - failed");
            }
        }
    }
}
