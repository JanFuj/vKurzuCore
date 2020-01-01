using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using vKurzuCore.Models;

namespace vKurzuCore.ViewModels.Dto
{
    public class BlogDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Zadejte název blogu")]
        [Display(Name = "Název")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Zadejte popis blogu")]
        [Display(Name = "Popis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Zadejte tělo blogu")]
        [Display(Name = "Tělo blogu")]
        public string Body { get; set; }

        [Display(Name = "Url titulek")]
        [Required(ErrorMessage = "Zadejte url titulek")]
        public string UrlTitle { get; set; }

        [Display(Name = "Souviející kurz")]
        public int RelatedCourseId { get; set; }
        public bool Approved { get; set; }

        [Display(Name = "Url obrazku v hlavnim layoutu")]
        public string HeaderImage { get; set; }

        [Display(Name = "Url obrazku pro sdílení na socíalech")]
        public string SocialSharingImage { get; set; }

        public List<BlogTag> BlogTags { get; set; } = new List<BlogTag>();
        public DateTime Changed { get; set; }

    }
}
