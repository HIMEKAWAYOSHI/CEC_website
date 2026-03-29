using System.Runtime.CompilerServices;
using CEC_website.Data;
using CEC_website.Models;
using Microsoft.AspNetCore.Mvc;

namespace CEC_website.Controllers
{
    public class DashboardController : Controller
    {

        private readonly AddPostEntriesTable _db;

        public DashboardController(AddPostEntriesTable db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout() {

            HttpContext.Session.Clear();

            return RedirectToAction("LoginPage", "Login");
        }

        public IActionResult Create()
        {

            List<PostEntry> PostedData = _db.PostDBs.ToList();
            


            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Dashboard", "index");
            }

            return View(PostedData);
        }
    }
}
