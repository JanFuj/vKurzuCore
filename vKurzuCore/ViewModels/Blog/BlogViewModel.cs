using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Models;
using vKurzuCore.ViewModels.Dto;

namespace vKurzuCore.ViewModels.Blog
{
    public class BlogViewModel
    {
        public BlogDto Blog { get; set; }
        public CourseDto RelatedCourse { get; set; }
        public List<CourseDto> Courses { get; set; }
        public string Tagy { get; set; }
        public List<Tag> Tags { get; set; }
        public List<BlogDto> RelatedArticles { get; set; }

    }
}
