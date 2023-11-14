using HotChocolate.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace MTAIntranetAngular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private const string URL = "http://mtadev.mta-flint.net:8000/fields";

        [HttpGet]
        public string GetFields()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept
                .Add(
                    new System.Net.Http.Headers
                    .MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(URL).Result;

            //return response.Content.ToString();

            if (response.IsSuccessStatusCode)
            {
                //return response.Content.ReadFromJsonAsync<DataObject>();
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<string>().Result;
                return dataObjects;
                //foreach (var d in dataObjects)
                //{
                //    Console.WriteLine("{0}", d.Id);
                //}
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
            return "Error";
        }
    }
}
