using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vKurzuCore.Services.Contracts;
using vKurzuCore.ViewModels.Dto;

namespace vKurzuCore.Controllers
{
    public class TutorialController : Controller
    {
        private readonly ITutorialCategoryService _tutorialCategoryService;
        private readonly IMapper _mapper;

        public TutorialController(
            ITutorialCategoryService tutorialCategoryService,
            IMapper mapper)
        {
            _tutorialCategoryService = tutorialCategoryService;
            _mapper = mapper;
        }
        //Get all tutorial categories
        [Route("tutorialy")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("tutorialy/{urlTitle}")]
        public async Task<IActionResult> Detail(string urlTitle, bool preview)
        {
            var category = await _tutorialCategoryService.FindPublishedByUrl(urlTitle, preview);
            if (category == null) return NotFound();
            var categoryDto = _mapper.Map<TutorialCategoryDto>(category);

            return View(categoryDto);
        }
    }
}