using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vKurzuCore.Repositories;
using vKurzuCore.Services;
using vKurzuCore.ViewModels.Course;

namespace vKurzuCore.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyEmailSender _emailSender;
        public CourseController(IUnitOfWork unitOfWork, IMyEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        [Route("kurz/{urlTitle}")]
        public async Task<IActionResult> Detail(string urlTitle, bool preview)
        {
            var course = preview ? await _unitOfWork.Courses.FindPreviewCourseByUrlAsync(urlTitle) : await _unitOfWork.Courses.FindByUrlAsync(urlTitle);
         
            if (course == null) return NotFound();
            var viewModel = new CourseViewModel()
            {
                Course = new ViewModels.Dto.CourseDto()
                {
                    Name = course.Name,
                    Body = course.Body,
                    WillLearn = course.WillLearn,
                    Description = course.Description,
                    Modificator = course.Modificator,
                    Svg = course.Svg,
                    SvgId = course.SvgId,
                    UrlTitle = course.UrlTitle
                },
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmail(CourseViewModel viewModel)
        {
            var course = await _unitOfWork.Courses.FindByUrlAsync(viewModel.Course.UrlTitle);
            var sent = await _emailSender.SendEmailFromForm(viewModel.FormModel.Email, $"Kurz: {course.Name}", $"Tento uživatel má zájem o kurz: {course.Name} \n {viewModel.FormModel.Name} {viewModel.FormModel.Surname} \n {viewModel.FormModel.Email}");
            if (!sent)
            {
                Console.WriteLine("sending email error");
            }
            else
            {
                TempData["EmailSent"] = true;
            }
            // zabrani opetovnemu odeslani formulare a presune na /kurz/{urlTitle},
            // jinak by zustal na /Course/SendEmail a po refreshi by znovu odeslal mail
            return RedirectToAction(nameof(Detail), new { urlTitle = course.UrlTitle });
        }

    }
}