using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.Models
{
    public class TutorialPost : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Body { get; set; }
        public string HeaderImage { get; set; }
        public string SocialSharingImage { get; set; }
        [Required]
        public string UrlTitle { get; set; }
        public int RelatedCourseId { get; set; }
        public List<TutorialPostTag> TutorialPostTags { get; set; }
        public int TutorialCategoryId { get; set; }
        public TutorialCategory Category { get; set; }
    }
}
