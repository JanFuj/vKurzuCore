using System.Collections.Generic;
using System.Linq;
using vKurzuCore.Data;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{

    public class SvgRepository : Repository<Svg>, ISvgRepository
    {
        public vKurzuDbContext vKurzuDbContext
        {
            get { return Context as vKurzuDbContext; }

        }
        public SvgRepository(vKurzuDbContext context) : base(context)
        {

        }
        public IEnumerable<Svg> GetAllSvgs()
        {
            return vKurzuDbContext.Svgs.Where(c => !c.Deleted).OrderBy(c => c.Position).ToList();
        }
    }
}
