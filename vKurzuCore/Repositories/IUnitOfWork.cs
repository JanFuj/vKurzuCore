using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminNoteRepository AdminNotes { get; }
        int Save();
    }
}
