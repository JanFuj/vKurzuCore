using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<Course> FindByUrlAsync(string urlTitle)
        {
            return vKurzuDbContext.Courses.Include(c => c.Svg).FirstOrDefaultAsync(c => !c.Deleted && c.Approved && c.UrlTitle == urlTitle);
        }
        public Task<Course> FindPreviewCourseByUrlAsync(string urlTitle)
        {
            return vKurzuDbContext.Courses.Include(c => c.Svg).FirstOrDefaultAsync(c => !c.Deleted  && c.UrlTitle == urlTitle);
        }
        public override Task<Course> FindByIdAsync(int id)
        {
            return vKurzuDbContext.Courses.Include("CourseTags.Tag")
                 .SingleOrDefaultAsync(x => x.Id == id && !x.Deleted);
        }
    }
}
