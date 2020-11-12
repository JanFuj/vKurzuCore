using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vKurzuCore.Repositories;
using vKurzuCore.ViewModels.Blog;
using vKurzuCore.ViewModels.Dto;

namespace vKurzuCore.Controllers
{

    public class BlogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BlogController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route("blog")]
        public IActionResult Index()
        {
            var blogs = _unitOfWork.Blogs.GetPublishedBlogArticles();
            var dtos = _mapper.Map<List<BlogDto>>(blogs);
            return View(dtos);
        }

        [Route("blog/{urlTitle}")]
        public async Task<IActionResult> Detail(string urlTitle, bool preview)
        {
            var blog = preview ? await _unitOfWork.Blogs.FindPreviewBlogByUrlAsync(urlTitle) : await _unitOfWork.Blogs.FindByUrlAsync(urlTitle);
            if (blog == null) return NotFound();
            var blogDto = _mapper.Map<BlogDto>(blog);
            ViewBag.Thumbnail = blogDto.SocialSharingImage;
            ViewBag.Description = blogDto.Description;
            var viewModel = new BlogViewModel()
            {
                Blog = blogDto,
                RelatedCourse = _mapper.Map<CourseDto>(await _unitOfWork.Courses.FindApprovedByIdAsync(blog.RelatedCourseId)),
                RelatedArticles = _mapper.Map<List<BlogDto>>(_unitOfWork.Blogs.GetRelatedArticles(blog.Id)),
            };
            return View(viewModel);
        }
    }
}