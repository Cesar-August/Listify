using Dashboard.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Models;

namespace TodoListApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Dashboard()
        {
            return View("~/Views/Dashboard/_DashboardContent.cshtml");
        }


        public IActionResult Cronograma()
        {
            return View("~/Views/Dashboard/_CronogramaContent.cshtml");
        }

        public IActionResult Indexdash()
        {
            return View();
        }
    }
}