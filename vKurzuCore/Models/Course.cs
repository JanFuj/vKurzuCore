using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.Models
{
    public class Course : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string WillLearn { get; set; }
        [Required]
        public string Body { get; set; }

        public int SvgId { get; set; }

        public Svg Svg { get; set; }

        [Required]
        public string Modificator { get; set; }

        [Required]
        public string UrlTitle { get; set; }
        public string HeaderImage { get; set; }
        public string SocialSharingImage { get; set; }
        public ICollection<CourseTag> CourseTags { get; set; }
    }
}
