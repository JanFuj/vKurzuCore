using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Helpers;
using vKurzuCore.Helpers.Contracts;
using vKurzuCore.Models;
using vKurzuCore.Repositories;
using vKurzuCore.ViewModels.Blog;
using vKurzuCore.ViewModels.Dto;

namespace vKurzuCore.Areas.Admin.Controllers
{
    [Authorize(Roles = Constants.Roles.Admin + "," + Constants.Roles.Lector)]
    [Area(Constants.Roles.Admin)]
    [Route("admin/blog")]
    public class BlogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string _loggedUserId;
        private readonly IMapper _mapper;
        private readonly ITagParser _tagParser;


        public BlogController(
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ITagParser tagParser)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _loggedUserId = _userManager.GetUserId(contextAccessor.HttpContext.User);
            _mapper = mapper;
            _tagParser = tagParser;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var blogs = await _unitOfWork.Blogs.GetAllAsync();
            if (User.IsInRole(Constants.Roles.Lector))
                blogs = blogs.Where(x => x.OwnerId == user.Id);

            var dtos = _mapper.Map<List<BlogDto>>(blogs);

            return View(dtos);
        }

        [Route("new")]
        public async Task<IActionResult> New()
        {
            var tags = await _unitOfWork.Tags.GetAllAsync();
            var courses = await _unitOfWork.Courses.GetAllAsync();
            var viewModel = new BlogViewModel()
            {
                Blog = new BlogDto(),
                Tags = tags.ToList(),
                Courses = courses.Select(course => new CourseDto() { Id = course.Id, Name = course.Name }).ToList(),
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("new")]
        public async Task<IActionResult> New(BlogViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newBlog = _mapper.Map<Blog>(viewModel.Blog);
                    newBlog.OwnerId = _loggedUserId;
                    var tagIds = await _tagParser.ParseTags(viewModel.Tagy);

                    tagIds.ForEach(id =>
                        newBlog.BlogTags.Add(new BlogTag()
                        {
                            TagId = id
                        }));
                    _unitOfWork.Blogs.Add(newBlog);
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException sqlEx) when (sqlEx.InnerException.HResult == (-2146232060))
            {
                ModelState.AddModelError("Blog.UrlTitle", "Zadane url již existuje");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            var tags = await _unitOfWork.Tags.GetAllAsync();
            var courses = await _unitOfWork.Courses.GetAllAsync();

            viewModel.Tags = tags.ToList();
            viewModel.Courses = courses.Select(course => new CourseDto() { Id = course.Id, Name = course.Name }).ToList();

            return View(viewModel);
        }

        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _unitOfWork.Blogs.FindByIdAsync(id);

            if (blog == null || (User.IsInRole(Constants.Roles.Lector) && blog.OwnerId != _loggedUserId)) return NotFound();

            var blogDto = _mapper.Map<BlogDto>(blog);
            var tags = await _unitOfWork.Tags.GetAllAsync();
            var courses = await _unitOfWork.Courses.GetAllAsync();
            var viewModel = new BlogViewModel()
            {
                Blog = blogDto,
                Tags = tags.ToList(),
                Courses = courses.Select(course => new CourseDto() { Id = course.Id, Name = course.Name }).ToList(),
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(BlogViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var blogToUpdate = await _unitOfWork.Blogs.FindByIdAsync(viewModel.Blog.Id);
                    if (blogToUpdate == null || (User.IsInRole(Constants.Roles.Lector) && blogToUpdate.OwnerId != _loggedUserId)) return NotFound();
                    _mapper.Map(viewModel.Blog, blogToUpdate);
                    var tagIds = await _tagParser.ParseTags(viewModel.Tagy);

                    tagIds.ForEach(id =>
                      blogToUpdate.BlogTags.Add(new BlogTag()
                      {
                          TagId = id,
                          BlogId = blogToUpdate.Id
                      }));
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException sqlEx) when (sqlEx.InnerException.HResult == (-2146232060))
            {
                ModelState.AddModelError("Blog.UrlTitle", "Zadane url již existuje");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            var tags = await _unitOfWork.Tags.GetAllAsync();
            var courses = await _unitOfWork.Courses.GetAllAsync();

            viewModel.Tags = tags.ToList();
            viewModel.Courses = courses.Select(course => new CourseDto() { Id = course.Id, Name = course.Name }).ToList();

            return View(viewModel);
        }

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var blogToDelete = await _unitOfWork.Blogs.FindByIdAsync(id);
            if (blogToDelete == null || (User.IsInRole(Constants.Roles.Lector) && blogToDelete.OwnerId != _loggedUserId)) return NotFound();

            var blogDto = new BlogDto()
            {
                Id = blogToDelete.Id,
                Name = blogToDelete.Name,
            };
            return View(blogDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(BlogDto blogDto)
        {
            var blogToDelete = await _unitOfWork.Blogs.FindByIdAsync(blogDto.Id);
            if (blogToDelete == null || (User.IsInRole(Constants.Roles.Lector) && blogToDelete.OwnerId != _loggedUserId)) return NotFound();
            _unitOfWork.Blogs.Remove(blogToDelete);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Constants.Roles.Admin)]
        [Route("approveblog")]
        public async Task<IActionResult> ApproveBlog(int id, bool approve)
        {
            try
            {
                var blogToApprove = await _unitOfWork.Blogs.FindByIdAsync(id);
                if (blogToApprove == null) return NotFound();

                blogToApprove.Approved = approve;
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