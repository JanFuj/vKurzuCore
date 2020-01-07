using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public interface ITutorialPostRepository : IRepository<TutorialPost>
    {
        Task<IEnumerable<TutorialPost>> GetPostsByCategoryAsync(int categoryId);
        Task<IEnumerable<TutorialPost>> GetApprovedPostsByCategoryAsync(int categoryId);
    }
}
