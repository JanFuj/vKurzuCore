using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Data;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public class ImageRepository : Repository<ImageFile>, IImageRepository
    {
        public vKurzuDbContext vKurzuDbContext
        {
            get { return Context as vKurzuDbContext; }

        }
        public ImageRepository(vKurzuDbContext context) : base(context)
        {

        }
        public IEnumerable<ImageFile> GetAllImages()
        {
            return vKurzuDbContext.ImageFiles.Where(c => !c.Deleted).OrderBy(c => c.Position).ToList();
        }
    }
}
