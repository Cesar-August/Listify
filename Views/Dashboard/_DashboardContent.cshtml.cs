using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dashboard.Views.Home
{
    // Renamed the class to avoid conflict with another "IndexModel" in the same namespace
    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}