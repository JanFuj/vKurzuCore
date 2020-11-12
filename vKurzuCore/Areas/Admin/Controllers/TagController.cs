using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class TagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Tag
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var tags = await _unitOfWork.Tags.GetAllAsync();
            var viewModels = tags.Select(
                tag => new TagViewModel() { Id = tag.Id, Name = tag.Name });
            return View(viewModels);
        }


        // GET: Admin/Tag/Create
        [Route("new")]
        public IActionResult New()
        {
            return View(new TagViewModel());
        }

        // POST: Admin/Tag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("new")]
        public async Task<IActionResult> New(TagViewModel tagViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tag = new Tag()
                    {
                        Name = tagViewModel.Name
                    };
                    _unitOfWork.Tags.Add(tag);
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException sqlEx) when (sqlEx.InnerException.HResult == (-2146232060))
            {
                ModelState.AddModelError("Name", "Zadany nazev již existuje");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.InnerException.Message);
            }
            return View(tagViewModel);
        }

        // GET: Admin/Tag/Edit/5
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var tag = await _unitOfWork.Tags.FindByIdAsync(id);

            if (tag == null) return NotFound();
            var viewModel = new TagViewModel();
            viewModel.Id = tag.Id;
            viewModel.Name = tag.Name;

            return View(viewModel);
        }

        // POST: Admin/Tag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(TagViewModel viewModel)
        {
            try
            {
                var tagToUpdate = await _unitOfWork.Tags.FindByIdAsync(viewModel.Id);
                if (tagToUpdate == null) return NotFound();

                tagToUpdate.Name = viewModel.Name;
                tagToUpdate.Changed = DateTime.Now;

                await _unitOfWork.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException sqlEx) when (sqlEx.InnerException.HResult == (-2146232060))
            {
                ModelState.AddModelError("Name", "Zadany nazev již existuje");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.InnerException.Message);
            }

            return View(viewModel);
        }

        // GET: Admin/Tag/Delete/5
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tagToDelete = await _unitOfWork.Tags.FindByIdAsync(id);
            if (tagToDelete == null) return NotFound();
            var viewModel = new TagViewModel();
            viewModel.Id = tagToDelete.Id;
            viewModel.Name = tagToDelete.Name;

            return View(viewModel);
        }

        // POST: Admin/Tag/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(TagViewModel viewModel)
        {
            try
            {
                var tagToDelete = await _unitOfWork.Tags.FindByIdAsync(viewModel.Id);
                if (tagToDelete == null) return NotFound();

                _unitOfWork.Tags.Remove(tagToDelete);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(viewModel);
            }
        }
    }
}
