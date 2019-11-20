using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using vKurzuCore.Helpers;
using vKurzuCore.Models;
using vKurzuCore.Repositories;
using vKurzuCore.ViewModels.Admin;

namespace vKurzuCore.Areas.Admin.Controllers
{
    [Authorize(Roles = Constants.Roles.Admin + "," + Constants.Roles.Lector)]
    [Area(Constants.Roles.Admin)]
    [Route("admin/[controller]")]
    public class SvgController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SvgController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Svgs.GetAllAsync()); ;
        }

        [Route("new")]
        public ActionResult New()
        {
            var viewModel = new SvgViewModel();
            return View(viewModel);
        }

        // POST: Svg/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("new")]
        public async Task<IActionResult> New(SvgViewModel svgViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View(svgViewModel);

                var svg = new Svg()
                {
                    Name = svgViewModel.Name,
                    Path = svgViewModel.Path,
                };

                _unitOfWork.Svgs.Add(svg);
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
            return View(svgViewModel);
        }

        // GET: Svg/Edit/5
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var svg = await _unitOfWork.Svgs.FindByIdAsync(id);

            if (svg == null || User.IsInRole(Constants.Roles.Lector)) return NotFound();
            var viewModel = new SvgViewModel();
            viewModel.Id = svg.Id;
            viewModel.Name = svg.Name;
            viewModel.Path = svg.Path;

            return View(viewModel);
        }

        // POST: Svg/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(SvgViewModel svgViewModel)
        {
            try
            {
                var svgToUpdate = await _unitOfWork.Svgs.FindByIdAsync(svgViewModel.Id);
                if (svgToUpdate == null) return NotFound();

                svgToUpdate.Name = svgViewModel.Name;
                svgToUpdate.Path = svgViewModel.Path;
                svgToUpdate.Changed = DateTime.Now;

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

            return View(svgViewModel);
        }

        // GET: Svg/Delete/5
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var svgToDelete = await _unitOfWork.Svgs.FindByIdAsync(id);
            if (svgToDelete == null || User.IsInRole(Constants.Roles.Lector)) return NotFound();
            var viewModel = new SvgViewModel();
            viewModel.Id = svgToDelete.Id;
            viewModel.Name = svgToDelete.Name;

            return View(viewModel);
        }

        // POST: Svg/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(SvgViewModel svgViewModel)
        {
            try
            {
                var svgToDelete = await _unitOfWork.Svgs.FindByIdAsync(svgViewModel.Id);
                if (svgToDelete == null || User.IsInRole(Constants.Roles.Lector)) return NotFound();

                _unitOfWork.Svgs.Remove(svgToDelete);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(svgViewModel);
            }
        }
    }
}