using CaseStudy.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class RestFullBookAPIPostTests : CoreCodes
    {
        [Test]
        [Order(1)]

        public void GenerateToken()
        {
            test = extent.CreateTest("Generate Token");
            Log.Information("Token Generated");
            var request = new RestRequest("auth", Method.Post);
            request.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });

            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode.Equals(System.Net.HttpStatusCode.OK));
                Log.Information("Response code is as expected");
                var token = JsonConvert.DeserializeObject<HeaderData>(response.Content);
                Assert.IsNotNull(token);
                Log.Information("response id returned" + token);
                Log.Information("All asserts passed for Generate Token Test");

                test.Pass("Generate Token Test - Passed");
            }
            catch (AssertionException ex)
            {
                test.Fail("Generate Token Test - failed" + ex.Message);
            }

        }

        [Test]
        [Order(2)]
       
        public void CreateBooking()
        {
            test = extent.CreateTest("Create Booking");
            Log.Information("Create Booking");
            
            var createBookingRequest = new RestRequest("booking", Method.Post);
            createBookingRequest.AddHeader("Content-Type", "application/json");
            createBookingRequest.AddHeader("Accept", "application/json");
            createBookingRequest.AddJsonBody(new
            {
                firstname = "Jim",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new { 
                    checkin = "2018-01-01",
                    checkout = "2019-01-01"
                },
                additionalneeds = "Breakfast"
            });
           
           
            var createBookingResponse = client.Execute(createBookingRequest);

            try
            {
                Assert.That(createBookingResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK));
                Log.Information("Response code is as expected");

                JObject data = JObject.Parse(createBookingResponse.Content);
                Assert.IsNotNull(createBookingResponse.Content);
                Log.Information("response id returned" + createBookingResponse.Content);
                Log.Information("All asserts passed for  Create Booking Test");

                test.Pass("Create Booking Test - Passed");
            }
            catch (AssertionException ex)
            {
                test.Fail("Create Booking Test - failed" + ex.Message);
            }

        }
    }
}
