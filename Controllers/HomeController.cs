using Microsoft.AspNetCore.Mvc;

namespace TodoListApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}