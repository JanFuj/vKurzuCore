using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.Models
{
    public class TutorialCategory : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UrlTitle { get; set; }
        public string HeaderImage { get; set; }
        public string SocialSharingImage { get; set; }
        public List<TutorialPost> Posts { get; set; } = new List<TutorialPost>();
    }
}
