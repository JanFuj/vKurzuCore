﻿using System.Collections.Generic;
using System.Threading.Tasks;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<Course> GetPublishedCourses();

        Task<Course> FindByUrlAsync(string urlTitle);
    }
}
