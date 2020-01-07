using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Models;
using vKurzuCore.ViewModels.Dto;

namespace vKurzuCore.ViewModels.TutorialPost
{
    public class TutorialPostViewModel
    {
        public TutorialPostDto Post { get; set; } = new TutorialPostDto();

        /// <summary>
        /// List of post in category
        /// </summary>
        public IEnumerable<TutorialPostDto> Posts { get; set; }
        public TutorialCategoryDto Category { get; set; }
        public string CategoryName { get; set; }
        public CourseDto RelatedCourse { get; set; }
        public List<CourseDto> Courses { get; set; }

        /// <summary>
        /// List of tags in view
        /// </summary>
        public List<Tag> Tags { get; set; }
        public string Tagy { get; set; }


    }
}
