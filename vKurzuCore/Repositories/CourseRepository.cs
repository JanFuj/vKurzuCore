using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using vKurzuCore.Data;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public vKurzuDbContext vKurzuDbContext
        {
            get { return Context as vKurzuDbContext; }

        }
        public CourseRepository(vKurzuDbContext context) : base(context)
        {

        }
        public IEnumerable<Course> GetPublishedCourses()
        {
            return vKurzuDbContext.Courses.Include(c => c.Svg).Where(c => !c.Deleted && c.Approved).OrderBy(c => c.Position).ToList();
        }
    }
}
