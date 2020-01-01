using System.Collections.Generic;
using System.Threading.Tasks;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public interface ITutorialCategoryRepository : IRepository<TutorialCategory>
    {
        Task<IEnumerable<TutorialCategory>> GetPublishedTutorialCategories();
    }
}
