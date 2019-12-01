using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vKurzuCore.Data;
using vKurzuCore.Helpers;
using vKurzuCore.Models;
using vKurzuCore.Repositories;
using vKurzuCore.ViewModels.Admin;
using vKurzuCore.ViewModels.Dto;

namespace vKurzuCore.Areas.Admin.Controllers
{
    [Authorize(Roles = Constants.Roles.Admin + "," + Constants.Roles.Lector)]
    [Area(Constants.Roles.Admin)]
    [Route("admin/kurz")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public CourseController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: Admin/Course
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var courses = await _unitOfWork.Courses.GetAllAsync();
            if (User.IsInRole(Constants.Roles.Lector))
            {
                courses = courses.Where(x => x.OwnerId == user.Id);
            }

            var dtos = courses.Select(course =>
            new CourseDto()
            {
                Id = course.Id,
                Name = course.Name,
                Approved = course.Approved,

            });
            return View(dtos);
        }

        //// GET: Admin/Course/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var courseDto = await _context.CourseDto
        //        .Include(c => c.Svg)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (courseDto == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(courseDto);
        //}

        // GET: Admin/Course/Create

        [Route("new")]
        public async Task<IActionResult> New()
        {
            var svgs = await _unitOfWork.Svgs.GetAllAsync();
            var tags = await _unitOfWork.Tags.GetAllAsync();
            var viewModel = new CourseViewModel()
            {
                Course = new CourseDto()
                {
                    Svg = svgs.First(),
                    SvgId = svgs.First().Id
                },
                Svgs = svgs.ToList(),
                Tags = tags.ToList()
            };
            return View(viewModel);
        }

        // POST: Admin/Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("new")]
        public async Task<IActionResult> New(CourseViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCourse = new Course()
                    {
                        Name = viewModel.Course.Name,
                        Description = viewModel.Course.Description,
                        WillLearn = viewModel.Course.WillLearn,
                        Body = viewModel.Course.Body,
                        UrlTitle = viewModel.Course.UrlTitle,
                        SocialSharingImage = viewModel.Course.SocialSharingImage,
                        Svg = await _unitOfWork.Svgs.FindByIdAsync(viewModel.Course.SvgId),
                        Modificator = viewModel.Course.Modificator,
                        Approved = false,
                    };

                    _unitOfWork.Courses.Add(newCourse);
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException sqlEx) when (sqlEx.InnerException.HResult == (-2146232060))
            {
                ModelState.AddModelError("Course.UrlTitle", "Zadane url již existuje");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.InnerException.Message);
            }
            var svgs = await _unitOfWork.Svgs.GetAllAsync();
            var tags = await _unitOfWork.Tags.GetAllAsync();
            viewModel.Svgs = svgs.ToList();
            viewModel.Tags = tags.ToList();
            return View(viewModel);
        }

        //// GET: Admin/Course/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var courseDto = await _context.CourseDto.FindAsync(id);
        //    if (courseDto == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["SvgId"] = new SelectList(_context.Svgs, "Id", "Name", courseDto.SvgId);
        //    return View(courseDto);
        //}

        //// POST: Admin/Course/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,WillLearn,Body,SvgId,Modificator,UrlTitle,HeaderImage,SocialSharingImage")] CourseDto courseDto)
        //{
        //    if (id != courseDto.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(courseDto);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CourseDtoExists(courseDto.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["SvgId"] = new SelectList(_context.Svgs, "Id", "Name", courseDto.SvgId);
        //    return View(courseDto);
        //}

        //// GET: Admin/Course/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var courseDto = await _context.CourseDto
        //        .Include(c => c.Svg)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (courseDto == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(courseDto);
        //}

        //// POST: Admin/Course/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var courseDto = await _context.CourseDto.FindAsync(id);
        //    _context.CourseDto.Remove(courseDto);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CourseDtoExists(int id)
        //{
        //    return _context.CourseDto.Any(e => e.Id == id);
        //}
    }
}
