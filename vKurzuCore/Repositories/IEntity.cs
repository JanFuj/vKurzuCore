using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.Repositories
{
    public interface IEntity : IEntity<long>
    {

    }

    /// <summary>
    /// Basic interface representing entity
    /// </summary>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// ID
        /// </summary>
        TKey Id { get; set; }
    }
}
