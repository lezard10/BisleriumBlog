using BIsleriumBlog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BIsleriumBlog.Services
{
    public interface INotificationService
    {
            Task CreateNotificationAsync(string userId, int blogId, string message);
    }
}
