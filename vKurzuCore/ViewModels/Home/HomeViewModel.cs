using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Models;
using vKurzuCore.ViewModels.Home;

namespace vKurzuCore.ViewModels
{
    public class HomeViewModel
    {
        public List<Models.Course> Courses { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<TutorialCategory> TutorialCategories { get; set; }
        public HomeContactForm FormModel { get; set; } = new HomeContactForm();
        public bool ShowAlert { get; set; } = false;
    }
}
