using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using CEC_website.Data;
using CEC_website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var posts = _db.PostDBs.OrderByDescending(a => a.DayPosted).ToList();


            foreach (var post in posts)
            {
                post.Content = post.Content.Replace("\n", "<br>");

                post.Content = Regex.Replace(post.Content,
                    @"(https?://[^\s]+)",
                    "<a href='$1' target='_blank'>$1</a>");

            }


            return View(posts);

        }

        public IActionResult Logout()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("LoginPage", "Login");
        }



        public IActionResult Create()
        {


            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(PostEntry post, IFormFile image)
        {

            if (post == null)
            {
                ViewBag.Error = "Something went wrong. Please try again!";
                return View();
            }
            // check if user is Admin
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index", "Dashboard");
            }

            // check if Title and Content are empty
            if (string.IsNullOrWhiteSpace(post.Title) || string.IsNullOrWhiteSpace(post.Content))
            {
                ViewBag.Error = "Title and Content cannot be empty!";
                return View();

            }

            // kani kai tig handle image upload
            if (image != null)
            {

                var ImageValidation = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                string extension = Path.GetExtension(image.FileName).ToLower();

                if (!ImageValidation.Contains(extension))
                {
                    ViewBag.Error = "The file only accepts (.jpg,  .jpeg,  .png,  .gif,  .webp)";
                    return View();
                }


                string folderpath = "wwwroot/AdminPost";

                // create folder if it doesn't exist
                if (!Directory.Exists(folderpath))
                {
                    Directory.CreateDirectory(folderpath);
                }

                // generate unique file name to avoid duplicates
                string fileName = Guid.NewGuid().ToString() + extension;
                string path = Path.Combine(folderpath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                post.ImagePath = "/AdminPost/" + fileName;
            }

            post.DayPosted = DateTime.Now;
            _db.PostDBs.Add(post);
            _db.SaveChanges();

            return RedirectToAction("Index", "Dashboard");
        }




        //edit controllers 
        public IActionResult Edit(int id)
        {
            var post = _db.PostDBs.Find(id);

            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("index", "Dashboard");
            }

            if (post == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View(post);
        }

        // POST - saves the updated data
        [HttpPost]
        public IActionResult Edit(PostEntry post)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("index", "Dashboard");
            }

            // diri mo check if tanan required fields kay na butang ba or di 
            var existing = _db.PostDBs.Find(post.Id);

            if (existing == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            
            existing.Title = string.IsNullOrWhiteSpace(post.Title)
                ? existing.Title
                : post.Title;

            existing.Content = string.IsNullOrWhiteSpace(post.Content)
                ? existing.Content  
                : post.Content;     

            
            _db.SaveChanges();

            return RedirectToAction("Index", "Dashboard");
        }
        




        //Delete controllers
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index", "Dashboard");
            }

            var post = _db.PostDBs.Find(id);
            
            if (post == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            // delete the image file from the server
            if (!string.IsNullOrEmpty(post.ImagePath))
            {
                string filePath = Path.Combine("wwwroot", post.ImagePath.TrimStart('/'));

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath); // kani ghe delet ang literal file bai
                }
            }

            _db.PostDBs.Remove(post);
            _db.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
        }



    }

}