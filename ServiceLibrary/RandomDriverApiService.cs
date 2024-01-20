using Newtonsoft.Json.Linq;
using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceLibrary
{
    public class RandomDriverApiService : IRandomDriverApiService
    {
        public async Task<Driver> GetRandomDriver()
        {
            var driver = new Driver();
            try
            {
                var url = $"https://randomuser.me/api/";
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage();
                    request.RequestUri = new Uri(url);
                    request.Method = HttpMethod.Get;
                    var response = await client.SendAsync(request);
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonObject = JObject.Parse(responseString);
                        driver = new Driver
                        {
                            GivenName = jsonObject["results"]![0]!["name"]!["first"]!.ToString(),
                            SurName = jsonObject["results"]![0]!["name"]!["last"]!.ToString(),
                        };
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }

            return driver;
        }

    }
}
