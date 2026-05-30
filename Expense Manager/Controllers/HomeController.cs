// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;

namespace Expense_Manager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Custom Security Check: If a user is NOT logged in, 
            // don't show them the home page; send them straight to the login screen!
            if (User.Identity?.IsAuthenticated != true)
            {
                return RedirectToAction("Login", "Account");
            }

            // If they are logged in, show them the dashboard view
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}