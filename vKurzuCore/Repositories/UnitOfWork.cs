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

        public ICourseRepository Courses { get; private set; }
        public ITutorialCategoryRepository TutorialCategories { get; private set; }
        public IBlogRepository Blogs { get; private set; }
        public ISvgRepository Svgs { get; private set; }
        public IImageRepository Images { get; private set; }

        private readonly vKurzuDbContext _context;
        public UnitOfWork(vKurzuDbContext context)
        {
            _context = context;
            AdminNotes = new AdminNoteRepository(_context);
            Courses = new CourseRepository(_context);
            TutorialCategories = new TutorialCategoryRepository(_context);
            Blogs = new BlogRepository(_context);
            Svgs = new SvgRepository(_context);
            Images = new ImageRepository(_context);
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
