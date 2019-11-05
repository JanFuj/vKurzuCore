using System.Collections.Generic;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public interface ITutorialCategoryRepository : IRepository<TutorialCategory>
    {
        IEnumerable<TutorialCategory> GetPublishedTutorialCategories();
    }
}
