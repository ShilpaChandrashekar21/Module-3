using CaseStudy.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CaseStudy.Tests
{
    [TestFixture]
    public class RestFulBookAPIGetTests : CoreCodes
    {
      
        [Test]
        [Order(1)]
         public void GetAllBookingIds()
        {
            test = extent.CreateTest("Get All Booking Ids");
            Log.Information("\n");
            Log.Information("Getting All Booking Ids");
            var getAllBookingIdRequest = new RestRequest("booking", Method.Get);
            var getAllBookingIdResponse = client.Execute(getAllBookingIdRequest);
            try
            {
                Assert.That(getAllBookingIdResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK));
                Log.Information("Response code is as expected");

                Assert.IsNotNull(getAllBookingIdResponse);
                Log.Information("response returned" + getAllBookingIdResponse);
                Log.Information("All assert for get all booking test passed");
                test.Pass("Get All Booking Ids - Passed");

            }
            catch(AssertionException ex)
            {
                test.Fail("Get All Booking Ids - Failed"+ex.Message);
            }
           

        }

        [Test,TestCase(13)]
        [Order(2)]
        public void GetSingleBookingId(int id)
        {
            test = extent.CreateTest("Get Single Booking Id");
            Log.Information("\n");
            Log.Information("Get Single Booking Id");
            var getSingleBookingIdRequest = new RestRequest("booking/"+id, Method.Get);
            getSingleBookingIdRequest.AddHeader("Accept", "application/json");
            var getSingleBookingIdResponse = client.Execute(getSingleBookingIdRequest);

            try
            {
                
                Assert.That(getSingleBookingIdResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK));
                Log.Information("Response code is as expected");

                var bookingData =  JsonConvert.DeserializeObject<BookingData>(getSingleBookingIdResponse.Content);
                Assert.IsNotNull(bookingData);
                Log.Information("response returned" + bookingData);
                Assert.IsNotEmpty(bookingData.FirstName);
                Log.Information("Field firstName is not empty");
                Assert.IsNotEmpty(bookingData.LastName);
                Log.Information("Field lastName is not empty");
                Assert.IsNotEmpty(bookingData.TotalPrice);
                Log.Information("Field totalPrice is not empty");
                Log.Information("All asserts of get single booking id test - passed");
                test.Pass("Get Single Booking Id - Passed");

            }
            catch(AssertionException ex)
            {
                test.Fail("Get Single Booking Id - Failed"+ex.Message);
            }
        }

    }
}
