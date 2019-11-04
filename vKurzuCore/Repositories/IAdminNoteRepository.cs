using System.Collections.Generic;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public interface IAdminNoteRepository : IRepository<AdminNote>
    {
        IEnumerable<AdminNote> GetAdminNotes();
    }
}
