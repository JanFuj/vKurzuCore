using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vKurzuCore.Data;
using vKurzuCore.Helpers;
using vKurzuCore.Models;
using vKurzuCore.Repositories;
using vKurzuCore.ViewModels.Admin;

namespace vKurzuCore.Areas.Admin.Controllers
{
    [Authorize(Roles = Constants.Roles.Admin + "," + Constants.Roles.Lector)]
    [Area(Constants.Roles.Admin)]
    [Route("admin/[controller]")]
    public class ImageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;

        public ImageController(
            IUnitOfWork unitOfWork,
            IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _environment = hostingEnvironment;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Images.GetAllAsync());
        }

        [Route("new")]
        public IActionResult New()
        {
            return View(new ImageViewModel());
        }

        // POST: Admin/Image/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("new")]
        public async Task<IActionResult> New(ImageViewModel imageViewModel)
        {
            if (imageViewModel.Thumbnails.Any())
            {
                foreach (var image in imageViewModel.Thumbnails)
                {

                    var uniqueFileName = GetUniqueFileName(image.FileName);
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    image.CopyTo(new FileStream(filePath, FileMode.Create));

                    var imageFile = new ImageFile();
                    imageFile.FileName = uniqueFileName;
                    _unitOfWork.Images.Add(imageFile);
                }
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imageViewModel);
        }

        // GET: Admin/Image/Edit/5
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var imageFile = await _unitOfWork.Images.FindByIdAsync((int)id);
            if (imageFile == null) return NotFound();

            return View(imageFile);
        }

        // POST: Admin/Image/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Path,FileName,Description")] ImageFile imageFile)
        {
            if (id != imageFile.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    //  _unitOfWork.Images.Update(imageFile);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageFileExists(imageFile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(imageFile);
        }

        // GET: Admin/Image/Delete/5
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var imageFile = await _unitOfWork.Images.FindByIdAsync((int)id);
            if (imageFile == null) return NotFound();

            return View(imageFile);
        }

        // POST: Admin/Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageFile = await _unitOfWork.Images.FindByIdAsync(id);
            _unitOfWork.Images.Remove(imageFile);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageFileExists(int id)
        {
            return _unitOfWork.Images.GetAllImages().ToList().Any(e => e.Id == id);
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
