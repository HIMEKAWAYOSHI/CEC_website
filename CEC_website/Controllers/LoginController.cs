using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CEC_website.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginPage(string username, string password)
        {
            string Username = (username ?? "").Trim();
            string Password = (password ?? "").Trim();

            if (Username == "CecAdmin2026" && Password == "Admin123456")
            {
                HttpContext.Session.SetString("Role", "Admin"); 
                return RedirectToAction("Index", "Dashboard");

            }

            if (Username == "CecStudent2026" && Password == "Student123456")
            {
                HttpContext.Session.SetString("Role", "Student");
                return RedirectToAction("Index", "Dashboard");
            }

            
            return View();
        }
    }
}
