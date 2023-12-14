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
    [TestFixture]
    public class RestFullBookAPIUpdateTests : CoreCodes
    {
        [Test]
        [Order(1)]
        [TestCase(13)]
        public void UpdateBooking(int id)
        {
            test = extent.CreateTest(" Update Booking");
            Log.Information("Updating Booking");
            var request = new RestRequest("auth", Method.Post);
            request.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });

            var response = client.Execute(request);
            
            var token = JsonConvert.DeserializeObject<HeaderData>(response.Content);
            Console.WriteLine(token);

            var updateRequest = new RestRequest("booking/" + id, Method.Put);
            updateRequest.AddHeader("Cookie", "token=" + token.Token);
            updateRequest.AddHeader("Content-Type", "application/json");
            updateRequest.AddHeader("Accept", "application/json");
            updateRequest.AddJsonBody(new
            {
                firstname = "Jim",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01"
                },
                additionalneeds = "Breakfast"
            });
            
            var updateResponse = client.Execute(updateRequest);

            try
            {
                Assert.That(updateResponse.StatusCode,Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("response code is as expected");

                var data = JsonConvert.DeserializeObject<BookingData>(updateResponse.Content);

                Assert.IsNotNull(data);
                Log.Information("response returned" + data);

                
                test.Pass("Update Booking test - Passed");
            }
            catch(AssertionException)
            {
                test.Fail("Update Booking test - Failed");
            }
        }

        [Test]
        [Order(2)]
        [TestCase(13)]
        public void PatchUpdateBooking(int id)
        {
            test = extent.CreateTest(" Update Booking");
            Log.Information("Updating Booking");
            var request = new RestRequest("auth", Method.Post);
            request.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });

            var response = client.Execute(request);

            var token = JsonConvert.DeserializeObject<HeaderData>(response.Content);

            var updateRequest = new RestRequest("booking/" + id, Method.Patch);
            updateRequest.AddHeader("Cookie", "token=" + token.Token);
            updateRequest.AddHeader("Content-Type", "application/json");
            updateRequest.AddHeader("Accept", "application/json");
            updateRequest.AddJsonBody(new
            {
                firstname = "Jim",
                lastname = "Black",
               
            });

            var updateResponse = client.Execute(updateRequest);

            try
            {
                Assert.That(updateResponse.StatusCode,Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("response code is as expected");

                var data = JsonConvert.DeserializeObject<BookingData>(updateResponse.Content);

                Assert.IsNotNull(data);
                Log.Information("response returned" + data);
                test.Pass("Patch Update Booking Test - Passed");
                
            }
            catch (AssertionException)
            {
                test.Fail("Patch Update Booking Test  - Failed");
            }
        }

    }
}
