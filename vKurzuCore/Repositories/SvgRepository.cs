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
    }
}
