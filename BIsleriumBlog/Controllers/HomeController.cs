using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using BIsleriumBlog.Models;
using BIsleriumBlog.Data;
using System.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly BIsleriumBlogContext _blogDB;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(BIsleriumBlogContext blogDB, UserManager<IdentityUser> userManager)
    {
        _blogDB = blogDB;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var blogs = _blogDB.Blogs.ToList();
        return View(blogs);
    }

    public IActionResult SortByPopularity()
    {
        var blogs = _blogDB.Blogs.OrderByDescending(b => (2 * b.Upvotes - b.Downvotes));
        return View("Index", blogs.ToList());
    }

    public IActionResult SortByRecency()
    {
        var blogs = _blogDB.Blogs.OrderByDescending(b => b.CreatedDate);
        return View("Index", blogs.ToList());
    }

    public IActionResult SortRandomly()
    {
        var blogs = _blogDB.Blogs.OrderBy(b => Guid.NewGuid());
        return View("Index", blogs.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Blogs blogs)
    {
        if (ModelState.IsValid)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            // Check if the user is authenticated
            if (currentUser != null)
            {
                string username = currentUser.Email;
                var atIndex = username.IndexOf('@');
                var displayUserName = atIndex != -1 ? username.Substring(0, atIndex) : username;
                blogs.UserId = displayUserName;

                _blogDB.Blogs.Add(blogs);
                await _blogDB.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                // Redirect to login or handle unauthenticated user
                return RedirectToAction("Login", "Account");
            }
        }
        return View(blogs);
    }


    public IActionResult Details(int id)
    {
        var blog = _blogDB.Blogs.FirstOrDefault(s => s.Id == id);
        return View(blog);
    }

    public IActionResult Delete(int id)
    {
        var blog = _blogDB.Blogs.FirstOrDefault(s => s.Id == id);
        return View(blog);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var blog = _blogDB.Blogs.FirstOrDefault(s => s.Id == id);
        _blogDB.Blogs.Remove(blog);
        _blogDB.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var blog = _blogDB.Blogs.Find(id);
        return View(blog);
    }

    [HttpPost]
    public IActionResult Edit(int id, Blogs blog)
    {
        if (ModelState.IsValid)
        {
            var existingBlog = _blogDB.Blogs.Find(id);

            // Update only the Title, Body, and ImageUrl properties
            existingBlog.Title = blog.Title;
            existingBlog.Body = blog.Body;
            existingBlog.ImageUrl = blog.ImageUrl;

            // Save changes to the database
            _blogDB.SaveChanges();

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

    [HttpPost]
    public async Task<IActionResult> Upvote(int id)
    {
        var blog = _blogDB.Blogs.FirstOrDefault(s => s.Id == id);
        var user = await _userManager.GetUserAsync(User);

        if (blog != null && user != null)
        {
            string userId = user.UserName;

            var existingVote = _blogDB.Votes.FirstOrDefault(v => v.UserId == userId && v.BlogId == id);

            if (existingVote == null)
            {
                blog.Upvotes++; // Increment upvotes
                _blogDB.Votes.Add(new Vote { UserId = userId, BlogId = id, IsUpvote = true });
            }
            else if (!existingVote.IsUpvote) // If the existing vote is a downvote, convert it to an upvote
            {
                blog.Upvotes++; // Increment upvotes
                blog.Downvotes--; // Decrement downvotes
                existingVote.IsUpvote = true;
            }

            _blogDB.SaveChanges();

        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Downvote(int id)
    {
        var blog = _blogDB.Blogs.FirstOrDefault(s => s.Id == id);
        var user = await _userManager.GetUserAsync(User);

        if (blog != null && user != null)
        {
            string userId = user.UserName;

            var existingVote = _blogDB.Votes.FirstOrDefault(v => v.UserId == userId && v.BlogId == id);

            if (existingVote == null)
            {
                blog.Downvotes++; // Increment downvotes
                _blogDB.Votes.Add(new Vote { UserId = userId, BlogId = id, IsUpvote = false });
            }
            else if (existingVote.IsUpvote) // If the existing vote is an upvote, convert it to a downvote
            {
                blog.Upvotes--; // Decrement upvotes
                blog.Downvotes++; // Increment downvotes
                existingVote.IsUpvote = false;
            }

            _blogDB.SaveChanges();
        }

        return RedirectToAction("Index");
    }
  
}
