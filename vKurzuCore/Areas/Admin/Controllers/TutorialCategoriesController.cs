using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using vKurzuCore.Helpers;
using vKurzuCore.Models;
using vKurzuCore.Services.Contracts;
using vKurzuCore.ViewModels.Dto;
using static vKurzuCore.Helpers.Constants;

namespace vKurzuCore.Areas.Admin.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.Lector)]
    [Area(Roles.Admin)]
    [Route("admin/tutorialCategories")]
    public class TutorialCategoriesController : Controller
    {
        private readonly ITutorialCategoryService _tutorialCategoryService;
        private readonly IMapper _mapper;

        public TutorialCategoriesController(
            ITutorialCategoryService tutorialCategoryService,
            IMapper mapper)
        {
            _tutorialCategoryService = tutorialCategoryService;
            _mapper = mapper;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var categories = await _tutorialCategoryService.GetAllAsync();
            var dtos = _mapper.Map<List<TutorialCategoryDto>>(categories);

            return View(dtos);
        }

        [Route("new")]
        public IActionResult New()
        {
            return View(new TutorialCategoryDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("new")]
        public async Task<IActionResult> New(TutorialCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return View(categoryDto);

            var newCategory = _mapper.Map<TutorialCategory>(categoryDto);
            var response = await _tutorialCategoryService.CreateAsync(newCategory);

            if (response.Success)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(response.ModelStateErrorKey, response.Message);
            return View(categoryDto);
        }

        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _tutorialCategoryService.FindByIdAsync(id);

            if (category == null) return NotFound();

            var categoryDto = _mapper.Map<TutorialCategoryDto>(category);

            return View(categoryDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(TutorialCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return View(categoryDto);

            var response = await _tutorialCategoryService.UpdateAsync(categoryDto);

            if (response == null) return NotFound();

            if (response.Success)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(response.ModelStateErrorKey, response.Message);
            return View(categoryDto);
        }

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _tutorialCategoryService.FindByIdAsync(id);

            if (category == null) return NotFound();

            var categoryDto = _mapper.Map<TutorialCategoryDto>(category);

            return View(categoryDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(TutorialCategoryDto categoryDto)
        {
            var response = await _tutorialCategoryService.DeleteAsync(categoryDto.Id);
            if (response == null) return NotFound();

            if (response.Success)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(response.ModelStateErrorKey, response.Message);
            return View(categoryDto);
        }

        [Authorize(Roles = Roles.Admin)]
        [Route("approvetutorialcategory")]
        public async Task<IActionResult> ApproveTutorialCategory(int id, bool approve)
        {
            var response = await _tutorialCategoryService.ApproveAsync(id, approve);
            if (response == null) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}