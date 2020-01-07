using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vKurzuCore.Services.Contracts;
using vKurzuCore.ViewModels.Dto;
using vKurzuCore.ViewModels.TutorialPost;
using static vKurzuCore.Helpers.Constants;

namespace vKurzuCore.Areas.Admin.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.Lector)]
    [Area(Roles.Admin)]
    [Route("admin/tutorialPosts/{categoryId}")]
    public class TutorialPostController : Controller
    {
        private ITutorialPostService _tutorialPostService;
        private readonly IMapper _mapper;

        public TutorialPostController(ITutorialPostService tutorialPostService, IMapper mapper)
        {
            _tutorialPostService = tutorialPostService;
            _mapper = mapper;
        }

        //Get all posts in category
        [Route("")]
        public async Task<IActionResult> Index(int categoryId)
        {
            var postsAndCategory = await _tutorialPostService.GetAllAsync(categoryId);
            if (postsAndCategory == null) return NotFound();
            var dtos = _mapper.Map<List<TutorialPostDto>>(postsAndCategory.Item2);
            var viewModel = new TutorialPostViewModel()
            {
                Posts = dtos,
                CategoryName = postsAndCategory.Item1.Name,
                Category = _mapper.Map<TutorialCategoryDto>(postsAndCategory.Item1),
            };
            return View(viewModel);
        }

        [Route("new")]
        public async Task<IActionResult> New(int categoryId)
        {
            var viewModel = await _tutorialPostService.GetNewPostViewModel(categoryId);
            if (viewModel == null) return NotFound();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("new")]
        public async Task<IActionResult> New(TutorialPostViewModel viewModel, int categoryId)
        {
            if (ModelState.IsValid)
            {
                viewModel.Post.TutorialCategoryId = categoryId;
                var response = await _tutorialPostService.CreateAsync(viewModel);
                if (response.Success)
                    return RedirectToAction(nameof(Index), new { categoryId = categoryId });
                ModelState.AddModelError(response.ModelStateErrorKey, response.Message);
            }

            var vm = await _tutorialPostService.GetNewPostViewModel(categoryId, viewModel.Post);
            if (vm == null) return NotFound();
            return View(vm);
        }
    }
}