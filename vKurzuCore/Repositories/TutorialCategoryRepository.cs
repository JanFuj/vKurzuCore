using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Data;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public class TutorialCategoryRepository : Repository<TutorialCategory>, ITutorialCategoryRepository
    {

        public vKurzuDbContext vKurzuDbContext
        {
            get { return Context as vKurzuDbContext; }

        }
        public TutorialCategoryRepository(vKurzuDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<TutorialCategory>> GetPublishedTutorialCategories()
        {
            var allCategories = await GetAllAsync();
            return allCategories.Where(c => c.Approved);
        }

        public async Task<TutorialCategory> GetPublishedCategoryDetailByUrl(string urlTitle)
        {
            var categoriesWithPosts = await vKurzuDbContext.TutorialCategories
                .Include(x => x.Posts)
                .FirstOrDefaultAsync(c => !c.Deleted && c.Approved && c.UrlTitle == urlTitle);
            categoriesWithPosts.Posts = categoriesWithPosts.Posts.Where(x => x.Approved && !x.Deleted).ToList();
            return categoriesWithPosts;
        }

        public async Task<TutorialCategory> GetPublishedCategoryDetailPreviewByUrl(string urlTitle)
        {
            var categoriesWithPosts = await vKurzuDbContext.TutorialCategories
             .Include(x => x.Posts)
             .FirstOrDefaultAsync(c => !c.Deleted && c.UrlTitle == urlTitle);
            categoriesWithPosts.Posts = categoriesWithPosts.Posts.Where(x => x.Approved && !x.Deleted).ToList();
            return categoriesWithPosts;
        }
    }
}
