using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.ViewModels.Dto
{
    public class TutorialCategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Zadejte název tutoriálu")]
        [Display(Name = "Název")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Zadejte popis kurzu")]
        [Display(Name = "Popis")]
        public string Description { get; set; }
        [Display(Name = "Url")]
        [Required(ErrorMessage = "Zadejte url")]
        public string UrlTitle { get; set; }
        [Display(Name = "Url obrazku v hlavnim layoutu")]
        public string HeaderImage { get; set; }
        [Display(Name = "Url obrazku pro sdílení na socíalech")]
        public string SocialSharingImage { get; set; }
        public bool Approved { get; set; }
    }
}
