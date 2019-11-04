using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Data;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public class AdminNoteRepository : Repository<AdminNote>, IAdminNoteRepository
    {
        
        public vKurzuDbContext vKurzuDbContext
        {
            get { return Context as vKurzuDbContext; }
           
        }
        public AdminNoteRepository(vKurzuDbContext context) : base(context)
        {

        }

        public IEnumerable<AdminNote> GetAdminNotes()
        {
            return vKurzuDbContext.AdminNotes.ToList();
        }
    }
}
