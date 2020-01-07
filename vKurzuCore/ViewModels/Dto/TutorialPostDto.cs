using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using vKurzuCore.Models;

namespace vKurzuCore.ViewModels.Dto
{
    public class TutorialPostDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Zadejte název článku")]
        [Display(Name = "Název")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Zadejte popis článku")]
        [Display(Name = "Popis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Zadejte tělo článku")]
        [Display(Name = "Tělo článku")]
        public string Body { get; set; }

        [Display(Name = "Url obrazku v hlavnim layoutu")]
        public string HeaderImage { get; set; }

        [Display(Name = "Url obrazku pro sdílení na socíalech")]
        public string SocialSharingImage { get; set; }

        [Display(Name = "Url titulek")]
        [Required(ErrorMessage = "Zadejte url titulek")]
        public string UrlTitle { get; set; }

        [Display(Name = "Souviející kurz")]
        public int RelatedCourseId { get; set; }
        public CourseDto RelatedCourse { get; set; }
        public List<TutorialPostTag> TutorialPostTags { get; set; } = new List<TutorialPostTag>();
        public int TutorialCategoryId { get; set; }
        public TutorialCategoryDto Category { get; set; }
        public int Position { get; set; }
        public bool Approved { get; set; }
    }
}
