using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using vKurzuCore.Helpers;
using vKurzuCore.Repositories;

namespace vKurzuCore.Controllers.Admin
{
    [Authorize(Roles = Contants.Admin + "," + Contants.Lector)]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var adminNotes = _unitOfWork.AdminNotes.GetAdminNotes().ToList().First();
            return View(adminNotes);
        }
    }
}