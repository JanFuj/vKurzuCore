using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Models;
using vKurzuCore.Services.Comunication;
using vKurzuCore.ViewModels.Dto;

namespace vKurzuCore.Services.Contracts
{
    public interface ITutorialCategoryService
    {
        Task<IEnumerable<TutorialCategory>> GetAllAsync();
        Task<IEnumerable<TutorialCategory>> GetAllPublished();
        Task<TutorialCategoryResponse> CreateAsync(TutorialCategory category);
        Task<TutorialCategory> FindByIdAsync(int id);
        Task<TutorialCategoryResponse> UpdateAsync(TutorialCategoryDto categoryDto);
        Task<TutorialCategoryResponse> DeleteAsync(int id);
        Task<TutorialCategoryResponse> ApproveAsync(int id, bool approve);
    }
}
