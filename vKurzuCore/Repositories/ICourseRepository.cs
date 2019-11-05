using System.Collections.Generic;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<Course> GetPublishedCourses();
    }
}
