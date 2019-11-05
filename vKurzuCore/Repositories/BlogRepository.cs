using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Data;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public vKurzuDbContext vKurzuDbContext
        {
            get { return Context as vKurzuDbContext; }

        }
        public BlogRepository(vKurzuDbContext context) : base(context)
        {

        }
        public IEnumerable<Blog> GetFirst3BlogPosts()
        {
            return vKurzuDbContext.Blogs.Include(b => b.BlogTags).Where(b => b.Approved && !b.Deleted).OrderBy(c => c.Position).Take(3).ToList();
        }
    }
}
