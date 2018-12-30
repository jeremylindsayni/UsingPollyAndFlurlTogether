using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WorldsWorstWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeekDayController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Manufacture 404, 503 and 504 errors for about a third of all responses
            var randomNumber = new Random();
            var randomInteger = randomNumber.Next(0, 8);

            switch (randomInteger)
            {
                case 0:
                    //Debug.WriteLine("[Webservice]: About to serve a 404...");
                    return StatusCode(StatusCodes.Status404NotFound);

                case 1:
                    //Debug.WriteLine("[Webservice]: About to serve a 503...");
                    return StatusCode(StatusCodes.Status503ServiceUnavailable);

                case 2:
                    //Debug.WriteLine("[Webservice]: About to sleep for 5 seconds before serving a 504...");
                    Thread.Sleep(5000);
                    //Debug.WriteLine("[Webservice]: About to serve a 504...");

                    return StatusCode(StatusCodes.Status504GatewayTimeout);
                default:
                {
                    var formattedCustomObject = JsonConvert.SerializeObject(
                        new
                        {
                            WeekDay = DateTime.Today.DayOfWeek.ToString()
                        });

                    //Debug.WriteLine("[Webservice]: About to correctly serve a 200 response");

                    return Ok(formattedCustomObject);
                }
            }
        }
    }
}