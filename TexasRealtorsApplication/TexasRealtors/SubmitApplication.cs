using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions;
using System.Collections.Generic;

namespace TexasRealtors
{
    public class ApplicationDocument
    {
        public string name { get; set; }
        public string address { get; set; }
        public string date { get; set; }
    };

    public static class SubmitApplication
    {
        [FunctionName("SubmitApplication")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB("ApplicationDatabase", "ApplicationContainer", ConnectionStringSetting = "CosmosDB")] out dynamic documentApplication,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            documentApplication = new { name = name, date = DateTime.Now, id = Guid.NewGuid() };

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";


            return (ActionResult)new OkObjectResult(responseMessage);
        }
    }
}
