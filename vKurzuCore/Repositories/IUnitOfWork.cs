using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminNoteRepository AdminNotes { get; }
        ICourseRepository Courses { get; }
        ITutorialCategoryRepository TutorialCategories { get; }
        ITutorialPostRepository TutorialPosts { get; }
        IBlogRepository Blogs { get; }
        ISvgRepository Svgs { get; }
        IImageRepository Images { get; }
        ITagRepository Tags { get; }

        int Save();
        Task<int> SaveAsync();
    }
}
