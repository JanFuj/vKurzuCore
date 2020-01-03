using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using vKurzuCore.Models;

namespace vKurzuCore.ViewModels.Dto
{
    public class TutorialPostDto
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
        public CourseDto RelatedCourse { get; set; }
        public List<TutorialPostTag> TutorialPostTags { get; set; }
        public int TutorialCategoryId { get; set; }
        public TutorialCategoryDto Category { get; set; }
        public int Position { get; set; }

    }
}
