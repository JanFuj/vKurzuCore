using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.ViewModels.Admin
{
    public class SvgViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Zadejte název")]
        [Display(Name = "Název")]
        public string Name { get; set; }
        [Display(Name = "Svg (<Path>...</Path>)")]
        [Required(ErrorMessage = "Zadejte svg (pouze Path.../Path)")]
        public string Path { get; set; }
    }
}
