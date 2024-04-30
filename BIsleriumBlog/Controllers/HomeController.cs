using BIsleriumBlog.Data;
using BIsleriumBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BIsleriumBlog.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly BIsleriumBlogContext blogDB;
        public HomeController(BIsleriumBlogContext blogDB)
        {
            this.blogDB = blogDB;
        }

        public IActionResult Index()
        {
            var blogs = blogDB.Blogs.ToList();
            return View(blogs);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Blogs blogs)
        {
            if (ModelState.IsValid)
            {
                blogDB.Blogs.Add(blogs);
                blogDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogs);
        }

        public IActionResult Details(int id)
        {
            var blog = blogDB.Blogs.FirstOrDefault(s => s.Id == id);
            return View(blog);
        }

        public IActionResult Delete(int id)
        {
            var blog = blogDB.Blogs.FirstOrDefault(s => s.Id == id);
            return View(blog);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var blog = blogDB.Blogs.FirstOrDefault(s => s.Id == id);
            blogDB.Blogs.Remove(blog);
            blogDB.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var student = blogDB.Blogs.Find(id);
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(int id, Blogs blog)
        {
            if (ModelState.IsValid)
            {
                blogDB.Blogs.Update(blog);
                blogDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
