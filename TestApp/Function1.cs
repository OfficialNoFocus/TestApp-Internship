using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.Http;

namespace TestApp
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            HttpClient Http = new HttpClient();

            IncidentsShow[] incidents = await Http.GetFromJsonAsync<IncidentsShow[]>("https://incidentenmelder-test-fa.azurewebsites.net/api/incidents?code=QEUlS8ZK4qfMEITWaEs8k4ScU2aHxJROSNJ4vBzFuzMPxTG/XxQ7kQ==");

            return new OkObjectResult(incidents);
        }
    }

    public class IncidentsShow
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public string Location { get; set; }
        public string Owner { get; set; }
        public DateTime CreationTime { get; set; }
        public string Status { get; set; }
        public string Weight { get; set; }
    }
}
