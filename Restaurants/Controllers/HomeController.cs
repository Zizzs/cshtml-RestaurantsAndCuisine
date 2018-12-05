using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Models;

namespace Restaurants.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
