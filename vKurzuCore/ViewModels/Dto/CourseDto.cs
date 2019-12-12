using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vKurzuCore.Models;

namespace vKurzuCore.ViewModels.Dto
{
    public class CourseDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Zadejte název kurzu")]
        [Display(Name = "Název")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Zadejte popis kurzu")]
        [Display(Name = "Popis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Zadejte co se v kurzu naučí")]
        [Display(Name = "Naučíte se - pouze odentrovat body")]
        public string WillLearn { get; set; }

        [Required(ErrorMessage = "Zadejte tělo kurzu")]
        [Display(Name = "Tělo kurzu - Mělo by obsahovat nadpisy - Proč se zůčastnit, Osnova, Kdy a kde")]
        public string Body { get; set; }

        [ForeignKey("Svg")]
        public int SvgId { get; set; }

        public Svg Svg { get; set; }

        [Display(Name = "Modificator")]
        [Required(ErrorMessage = "Zadejte modifikator")]
        public string Modificator { get; set; }

        [Display(Name = "Url titulek")]
        [Required(ErrorMessage = "Zadejte url titulek")]
        public string UrlTitle { get; set; }

        [Display(Name = "Url obrazku v hlavnim layoutu")]
        public string HeaderImage { get; set; }

        [Display(Name = "Url obrazku pro sdílení na socíalech")]
        public string SocialSharingImage { get; set; }
        public bool Approved { get; set; }
        public List<CourseTag> CourseTags { get; set; } = new List<CourseTag>();
    }
}
