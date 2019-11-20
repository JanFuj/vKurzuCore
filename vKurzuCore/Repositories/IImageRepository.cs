using System.Collections.Generic;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
   public interface IImageRepository : IRepository<ImageFile>
    {
        IEnumerable<ImageFile> GetAllImages();
    }
}
