using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using vKurzuCore.Helpers.Contracts;
using vKurzuCore.Models;
using vKurzuCore.Repositories;
using vKurzuCore.Services.Comunication;
using vKurzuCore.ViewModels.Dto;
using vKurzuCore.ViewModels.TutorialPost;

namespace vKurzuCore.Services.Contracts
{
    public class TutorialPostService : ITutorialPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _loggedUserId;
        private readonly ClaimsPrincipal _loggedUser;
        private readonly IMapper _mapper;
        private readonly ITagParser _tagParser;


        public TutorialPostService(IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ITagParser tagParser)
        {
            _unitOfWork = unitOfWork;
            _loggedUserId = userManager.GetUserId(contextAccessor.HttpContext.User);
            _loggedUser = contextAccessor.HttpContext.User;
            _mapper = mapper;
            _tagParser = tagParser;
        }

        public async Task<Response> CreateAsync(TutorialPostViewModel viewModel)
        {
            var category = await GetCategoryAsync(viewModel.Post.TutorialCategoryId);
            if (category == null)
                return new Response($"Kategorie nebyla nalezena", "");
            if (!await IsUrlUniqueInCategory(viewModel.Post))
                return new Response($"Zadane url již existuje", "Post.UrlTitle");
            try
            {
                var newPost = _mapper.Map<TutorialPost>(viewModel.Post);
                newPost.OwnerId = _loggedUserId;
                var tagIds = await _tagParser.ParseTags(viewModel.Tagy);
                tagIds.ForEach(id =>
                        newPost.TutorialPostTags.Add(new TutorialPostTag()
                        {
                            TagId = id
                        }));
                _unitOfWork.TutorialPosts.Add(newPost);
                await _unitOfWork.SaveAsync();
                return new Response();
            }
            catch (Exception ex)
            {
                return new Response($"An error occurred when saving the category: {ex.Message}", "");
            }
        }

        private async Task<bool> IsUrlUniqueInCategory(TutorialPostDto post)
        {
            var existingPost = await _unitOfWork.TutorialPosts.FindFirstOrDefaultAsync(x => x.UrlTitle == post.UrlTitle);

            return existingPost == null || existingPost?.Id == post.Id;
        }

        public async Task<Tuple<TutorialCategory, IEnumerable<TutorialPost>>> GetAllAsync(int categoryId)
        {
            var category = await _unitOfWork.TutorialCategories.FindByIdAsync(categoryId);
            if (category == null)
                return null;
            var posts = await _unitOfWork.TutorialPosts.GetPostsByCategoryAsync(categoryId);
            return new Tuple<TutorialCategory, IEnumerable<TutorialPost>>(category, posts);
        }

        public async Task<TutorialCategory> GetCategoryAsync(int categoryId)
        {
            return await _unitOfWork.TutorialCategories.FindByIdAsync(categoryId);
        }

        public async Task<TutorialPostViewModel> GetNewPostViewModel(int categoryId, TutorialPostDto existingPost = null)
        {
            var category = await GetCategoryAsync(categoryId);
            if (category == null)
                return null;

            var tags = await _unitOfWork.Tags.GetAllAsync();
            var courses = await _unitOfWork.Courses.GetAllAsync();
            var post = new TutorialPostDto() { Category = _mapper.Map<TutorialCategoryDto>(category) };
            if (existingPost != null)
            {
                existingPost.Category = post.Category;
                post = existingPost;
            }
            var viewModel = new TutorialPostViewModel()
            {
                CategoryName = category.Name,
                Post = post,
                Tags = tags.ToList(),
                Courses = courses.Select(course => new CourseDto() { Id = course.Id, Name = course.Name }).ToList(),
            };

            return viewModel;
        }
    }
}
