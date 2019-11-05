﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.ViewModels.Home
{
    public class HomeContactForm
    {
        [Required(ErrorMessage = "Zadejte email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Nesprávný email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Zadejte zprávu")]
        [Display(Name = "Zpráva")]
        public string Message { get; set; }
    }
}
