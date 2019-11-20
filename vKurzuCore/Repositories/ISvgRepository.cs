using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public interface ISvgRepository : IRepository<Svg>
    {
        IEnumerable<Svg> GetAllSvgs();
    }
}
