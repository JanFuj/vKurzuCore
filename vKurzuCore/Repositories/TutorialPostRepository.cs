using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Data;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public class TutorialPostRepository : Repository<TutorialPost>, ITutorialPostRepository
    {
        public vKurzuDbContext vKurzuDbContext
        {
            get { return Context as vKurzuDbContext; }
        }
        public TutorialPostRepository(vKurzuDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<TutorialPost>> GetPostsByCategoryAsync(int categoryId)
        {
            return await vKurzuDbContext.TutorialPosts
                .Include(x=>x.Category)
                .Include("TutorialPostTags.Tag")
                .Where(x => x.TutorialCategoryId == categoryId && !x.Deleted).ToListAsync();
        }
        public async Task<IEnumerable<TutorialPost>> GetApprovedPostsByCategoryAsync(int categoryId)
        {
            return await vKurzuDbContext.TutorialPosts
                .Include(x => x.Category)
                .Include("TutorialPostTags.Tag")
                .Where(x => x.TutorialCategoryId == categoryId && !x.Deleted && x.Approved).ToListAsync();
        }
        public override Task<TutorialPost> FindByIdAsync(int id)
        {
            return vKurzuDbContext.TutorialPosts.Include("TutorialPostTags.Tag")
                 .SingleOrDefaultAsync(x => x.Id == id && !x.Deleted);
        }

        public Task<TutorialPost> FindByIdAsyncIncludeCategory(int id)
        {
            return vKurzuDbContext.TutorialPosts.Include(x=>x.Category)
                 .SingleOrDefaultAsync(x => x.Id == id && !x.Deleted);
        }
    }
}
