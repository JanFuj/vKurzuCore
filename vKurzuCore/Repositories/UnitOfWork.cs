using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Data;

namespace vKurzuCore.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAdminNoteRepository AdminNotes { get; private set; }
        private readonly vKurzuDbContext _context;
        public UnitOfWork(vKurzuDbContext context)
        {
            _context = context;
            AdminNotes = new AdminNoteRepository(_context);
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
