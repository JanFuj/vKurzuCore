using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.ViewModels.Dto;

namespace vKurzuCore.ViewModels.Course
{
    public class CourseViewModel
    {
        public CourseDto Course { get; set; }
        public CourseContactForm FormModel { get; set; } = new CourseContactForm();
    }
}
