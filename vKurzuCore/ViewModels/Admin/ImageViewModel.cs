using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace vKurzuCore.ViewModels.Admin
{
    public class ImageViewModel
    {
        [Display(Name = "Vyber obrázky")]
        [Required(ErrorMessage = "Vyber obrázky")]
        public IFormFile[] Thumbnails { get; set; }
    }
}
