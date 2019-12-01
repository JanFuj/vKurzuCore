using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly string _loggedUserId;

        public CourseController(IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _loggedUserId = _userManager.GetUserId(contextAccessor.HttpContext.User);
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
                        OwnerId = _loggedUserId,
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

        // GET: Admin/Course/Edit/5
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _unitOfWork.Courses.FindByIdAsync(id);

            if (course == null || (User.IsInRole(Constants.Roles.Lector) && course.OwnerId != _loggedUserId)) return NotFound();

            var courseDto = new CourseDto()
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                WillLearn = course.WillLearn,
                Body = course.Body,
                UrlTitle = course.UrlTitle,
                SocialSharingImage = course.SocialSharingImage,
                SvgId = course.SvgId,
                Modificator = course.Modificator,
                Approved = false,
            };
            var svgs = await _unitOfWork.Svgs.GetAllAsync();
            var tags = await _unitOfWork.Tags.GetAllAsync();

            var viewModel = new CourseViewModel()
            {
                Course = courseDto,
                Tags = tags.ToList(),
                Svgs = svgs.ToList(),
            };
            return View(viewModel);
        }

        // POST: Admin/Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(CourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var courseToUpdate = await _unitOfWork.Courses.FindByIdAsync(viewModel.Course.Id);
                    if (courseToUpdate == null || (User.IsInRole(Constants.Roles.Lector) && courseToUpdate.OwnerId != _loggedUserId)) return NotFound();
                    courseToUpdate.Id = viewModel.Course.Id;
                    courseToUpdate.Name = viewModel.Course.Name;
                    courseToUpdate.Description = viewModel.Course.Description;
                    courseToUpdate.WillLearn = viewModel.Course.WillLearn;
                    courseToUpdate.Body = viewModel.Course.Body;
                    courseToUpdate.UrlTitle = viewModel.Course.UrlTitle;
                    courseToUpdate.SocialSharingImage = viewModel.Course.SocialSharingImage;
                    courseToUpdate.SvgId = viewModel.Course.SvgId;
                    courseToUpdate.Modificator = viewModel.Course.Modificator;

                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException sqlEx) when (sqlEx.InnerException.HResult == (-2146232060))
                {
                    ModelState.AddModelError("Course.UrlTitle", "Zadane url již existuje");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.InnerException.Message);
                }
            }

            var svgs = await _unitOfWork.Svgs.GetAllAsync();
            var tags = await _unitOfWork.Tags.GetAllAsync();
            viewModel.Svgs = svgs.ToList();
            viewModel.Tags = tags.ToList();
            return View(viewModel);
        }

        // GET: Admin/Course/Delete/5
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var courseToDelete = await _unitOfWork.Courses.FindByIdAsync(id);
            if (courseToDelete == null || (User.IsInRole(Constants.Roles.Lector) && courseToDelete.OwnerId != _loggedUserId)) return NotFound();

            var courseDto = new CourseDto()
            {
                Id = courseToDelete.Id,
                Name = courseToDelete.Name,
            };
            return View(courseDto);
        }

        // POST: Admin/Course/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(CourseDto courseDto)
        {
            var courseToDelete = await _unitOfWork.Courses.FindByIdAsync(courseDto.Id);
            if (courseToDelete == null || (User.IsInRole(Constants.Roles.Lector) && courseToDelete.OwnerId != _loggedUserId)) return NotFound();
            _unitOfWork.Courses.Remove(courseToDelete);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Constants.Roles.Admin)]
        [Route("approvecourse")]
        public async Task<IActionResult> ApproveCourse(int id, bool approve)
        {
            try
            {
                var courseToApprove = await _unitOfWork.Courses.FindByIdAsync(id);
                if (courseToApprove == null) return NotFound();

                courseToApprove.Approved = approve;
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
