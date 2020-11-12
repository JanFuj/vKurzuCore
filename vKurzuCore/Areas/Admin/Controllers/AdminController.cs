using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Helpers;
using vKurzuCore.Models;
using vKurzuCore.Repositories;

namespace vKurzuCore.Areas.Admin.Controllers
{
    [Area(Constants.Roles.Admin)]
    [Authorize(Roles = Constants.Roles.Admin + "," + Constants.Roles.Lector)]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var adminNotes = await _unitOfWork.AdminNotes.GetAllAsync();
            var adminNote = adminNotes.ToList().FirstOrDefault();
            return View(adminNote);
        }

        [HttpPost]
        public async Task<IActionResult> AdminNote(AdminNote adminNote)
        {
            var aNotes = await _unitOfWork.AdminNotes.GetAllAsync();
            var aNote = aNotes.FirstOrDefault();
            if(aNote == null)
            {
                _unitOfWork.AdminNotes.Add(new AdminNote
                {
                    Note = adminNote.Note
                });
            }
            else
            {
                aNote.Note = adminNote.Note;
            }
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}