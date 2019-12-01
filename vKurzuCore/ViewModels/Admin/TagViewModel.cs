using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.ViewModels.Admin
{
    public class TagViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Zadejte název tagu bez mezer - MujNovyTag")]
        [Display(Name = "Tag zadejte ve formátu - MůjNovýTag")]
        public string Name { get; set; }
    }
}
