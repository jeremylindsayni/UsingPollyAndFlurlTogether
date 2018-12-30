using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;

namespace MyWebsite.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                var weekday = await "https://localhost:44357/api/weekday"
                    .GetJsonAsync<WeekdayModel>();

                Debug.WriteLine("[App]: successful");

                return View(weekday);
            }
            catch (Exception e)
            {
                Debug.WriteLine("[App]: Failed - " + e.Message);
                throw;
            }
        }
    }
}