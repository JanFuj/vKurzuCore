using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.ViewModels.Admin
{
    public class PersonViewModel
    {
        public List<IdentityUser> Admins { get; set; }
        public List<IdentityUser> Lectors { get; set; }
        public List<IdentityUser> Users { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
