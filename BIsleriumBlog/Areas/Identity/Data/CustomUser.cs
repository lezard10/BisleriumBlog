using Microsoft.AspNetCore.Identity;

namespace BIsleriumBlog.Areas.Identity.Data
{
    public class CustomUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
