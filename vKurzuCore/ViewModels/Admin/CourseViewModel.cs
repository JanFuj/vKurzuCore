using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Models;
using vKurzuCore.ViewModels.Dto;

namespace vKurzuCore.ViewModels.Admin
{
    public class CourseViewModel
    {
        public CourseDto Course { get; set; }
        public string Section { get; set; }
        public string Tagy { get; set; } = "";
        public List<Svg> Svgs { get; set; }
        public List<Tag> Tags { get; set; }
        public string[] SelectedTags { get; set; }

    }
}
