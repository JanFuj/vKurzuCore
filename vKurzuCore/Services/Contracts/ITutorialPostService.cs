using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Models;
using vKurzuCore.Services.Comunication;
using vKurzuCore.ViewModels.Dto;
using vKurzuCore.ViewModels.TutorialPost;

namespace vKurzuCore.Services.Contracts
{
    public interface ITutorialPostService
    {
        /// <summary>
        /// Gets all posts according to category
        /// </summary>
        Task<Tuple<TutorialCategory, IEnumerable<TutorialPost>>> GetAllAsync(int categoryId);

        /// <summary>
        /// Gets given tutorial category
        /// </summary>
        Task<TutorialCategory> GetCategoryAsync(int categoryId);

        /// <summary>
        /// Gets viewmodel for new tutorial post
        /// </summary>
        /// <returns></returns>
        Task<TutorialPostViewModel> GetNewPostViewModel(int categoryId, TutorialPostDto existingPost = null);
        Task<Response> CreateAsync(TutorialPostViewModel viewModel);
        Task<TutorialPostViewModel> GetEditPostViewModel(int id, TutorialPostDto existingPost = null);
        Task<Response> UpdateAsync(TutorialPostViewModel viewModel);
        Task<TutorialPost> FindByIdAsync(int id,bool includeCategory = false);
        Task<Response> DeleteAsync(int id);
    }
}
