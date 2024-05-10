namespace BIsleriumBlog.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int BlogId { get; set; }
        public bool IsUpvote { get; set; }
    }
}
