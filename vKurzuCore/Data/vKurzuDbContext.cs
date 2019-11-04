using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vKurzuCore.Models;

namespace vKurzuCore.Data
{
    public class vKurzuDbContext : IdentityDbContext
    {
        public vKurzuDbContext(DbContextOptions<vKurzuDbContext> options)
            : base(options)
        {
        }
        public DbSet<AdminNote> AdminNotes { get; set; }
    }
}
