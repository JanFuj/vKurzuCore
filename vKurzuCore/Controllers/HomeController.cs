using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vKurzuCore.Models;
using vKurzuCore.Repositories;
using vKurzuCore.Services;
using vKurzuCore.Services.Contracts;
using vKurzuCore.ViewModels;

namespace vKurzuCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyEmailSender _emailSender;
        private readonly ITutorialCategoryService _tutorialCategoryService;
        private readonly HomeViewModel _viewModel;

        public HomeController(
            ILogger<HomeController> logger,
            IMyEmailSender emailSender,
            IUnitOfWork unitOfWork,
            ITutorialCategoryService tutorialCategoryService)
        {
            _logger = logger;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
            _viewModel = new HomeViewModel();
            _tutorialCategoryService = tutorialCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            _viewModel.Courses = _unitOfWork.Courses.GetPublishedCourses().ToList();
            var tutorialCategories = await _tutorialCategoryService.GetAllPublished();
            _viewModel.TutorialCategories = tutorialCategories.ToList();
            _viewModel.Blogs = _unitOfWork.Blogs.GetFirst3BlogPosts().ToList();
            _viewModel.ShowAlert = !string.IsNullOrEmpty(TempData["EmailSent"]?.ToString());
            return View(_viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmail(HomeViewModel viewModel)
        {
            var sent = await _emailSender.SendEmailFromForm(viewModel.FormModel.Email, "Dotaz", $"{viewModel.FormModel.Message} \n {viewModel.FormModel.Email}");
            if (!sent)
            {
                Console.WriteLine("sending email error");
            }
            else
            {
                TempData["EmailSent"] = "sent";
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
