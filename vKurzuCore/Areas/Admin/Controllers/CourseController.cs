using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vKurzuCore.Helpers;
using vKurzuCore.Helpers.Contracts;
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
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string _loggedUserId;
        private readonly ITagParser _tagParser;

        public CourseController(
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            ITagParser tagParser)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _loggedUserId = _userManager.GetUserId(contextAccessor.HttpContext.User);
            _tagParser = tagParser;
        }

        // GET: Admin/Course
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var courses = await _unitOfWork.Courses.GetAllAsync();
            if (User.IsInRole(Constants.Roles.Lector))
                courses = courses.Where(x => x.OwnerId == user.Id);

            var dtos = _mapper.Map<List<CourseDto>>(courses);

            return View(dtos);
        }

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
                    SvgId = svgs.First().Id,
                },
                Svgs = svgs.ToList(),
                Tags = tags.ToList()
            };
            return View(viewModel);
        }

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
                    };
                    var tagIds = await _tagParser.ParseTags(viewModel.Tagy);

                    tagIds.ForEach(id =>
                        newCourse.CourseTags.Add(new CourseTag()
                        {
                            TagId = id
                        }));

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
                ModelState.AddModelError("", e.Message);
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

            var courseDto = _mapper.Map<CourseDto>(course);
         
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
                    courseToUpdate.CourseTags = new List<CourseTag>();
                    var tagIds = await _tagParser.ParseTags(viewModel.Tagy);

                    tagIds.ForEach(id =>
                        courseToUpdate.CourseTags.Add(new CourseTag()
                        {
                            TagId = id,
                            CourseId = courseToUpdate.Id
                        }));
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException sqlEx) when (sqlEx.InnerException.HResult == (-2146232060))
                {
                    ModelState.AddModelError("Course.UrlTitle", "Zadane url již existuje");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
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
