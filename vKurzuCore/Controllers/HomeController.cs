using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vKurzuCore.Models;
using vKurzuCore.Repositories;
using vKurzuCore.ViewModels;

namespace vKurzuCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HomeViewModel _viewModel;

        public HomeController(
            ILogger<HomeController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _viewModel = new HomeViewModel();
        }

        public IActionResult Index()
        {
            _viewModel.Courses = _unitOfWork.Courses.GetPublishedCourses().ToList();
            _viewModel.TutorialCategories = _unitOfWork.TutorialCategories.GetPublishedTutorialCategories().ToList();
            _viewModel.Blogs = _unitOfWork.Blogs.GetFirst3BlogPosts().ToList();
            _viewModel.ShowAlert = !string.IsNullOrEmpty(TempData["EmailSent"]?.ToString());
            return View(_viewModel);
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
