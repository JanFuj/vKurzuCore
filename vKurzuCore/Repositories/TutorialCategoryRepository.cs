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

        public IEnumerable<TutorialCategory> GetPublishedTutorialCategories()
        {
            return vKurzuDbContext.TutorialCategories.Where(c => c.Approved && !c.Deleted).OrderBy(c => c.Position);
        }
    }
}
