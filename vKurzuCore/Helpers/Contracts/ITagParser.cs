using System.Collections.Generic;
using System.Threading.Tasks;
using vKurzuCore.Models;

namespace vKurzuCore.Helpers.Contracts
{
    public interface ITagParser
    {
        Task<List<int>> ParseTags(string tagNames);
    }
}
