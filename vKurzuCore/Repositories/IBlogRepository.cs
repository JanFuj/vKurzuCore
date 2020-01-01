using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        IEnumerable<Blog> GetFirst3BlogPosts();
        Task<Blog> FindByUrlAsync(string urlTitle);
        Task<Blog> FindPreviewBlogByUrlAsync(string urlTitle);
        IEnumerable<Blog> GetPublishedBlogArticles();
        IEnumerable<Blog> GetRelatedArticles(int id);
    }
}
