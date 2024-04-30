using System.ComponentModel.DataAnnotations;

namespace BIsleriumBlog.Models
{
    public class Blogs
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
