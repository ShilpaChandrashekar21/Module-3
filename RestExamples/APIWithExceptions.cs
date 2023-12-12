using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExamples
{
    public class APIWithExceptions
    {
        string baseUrl = "https://reqres.in/api/";
        
        //error 
        /* public void GetSingleUser()
         {
             var client = new RestClient(baseUrl);

             var req = new RestRequest("user/25", Method.Get);

             var response = client.Execute(req);

             if(!response.IsSuccessful)
             {
                 try
                 {
                     var errorDetails = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);

                     if(errorDetails != null)
                     {
                         Console.WriteLine($"API Error : {errorDetails.Error}");
                     }
                 }
                 catch(ArgumentNullException ex)
                 {
                     Console.WriteLine("Failed to deserialize error response "+ex.Message );
                 }
             }
             else
             {
                 Console.WriteLine("Successful Response");
                 JObject res = JObject.Parse(response.Content);
                 Console.WriteLine(res);
             }

         }
     }
 }*/
        //JSon body check


        public void GetSingleUser()
        {
            var client = new RestClient(baseUrl);

            var req = new RestRequest("user/25", Method.Get);

            var response = client.Execute(req);

            if (!response.IsSuccessful)
            {
                if(IsJson(response.Content))
                {
                    try
                    {
                        var errorDetails = JsonConvert.DeserializeObject<ErrorResponse>
                            (response.Content);

                        if (errorDetails != null)
                        {
                            Console.WriteLine(response.Content);
                            Console.WriteLine($"API Error : {errorDetails.Error}");
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine("Failed to deserialize error response " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("The response content is  null "+response.Content);
                }
                
            }
            else
            {
                Console.WriteLine("Successful Response");
                JObject res = JObject.Parse(response.Content);
                Console.WriteLine(res);
            }

        }

        static bool IsJson(string content)
        {
            try
            {
                JToken.Parse(content);
                return true;
            }
            catch(ArgumentNullException ex)
            {
                return false;
            }
            
        }
    }
}
