using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Data;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public vKurzuDbContext vKurzuDbContext
        {
            get { return Context as vKurzuDbContext; }
        }

        public TagRepository(vKurzuDbContext context) : base(context)
        {

        }
    }
}
