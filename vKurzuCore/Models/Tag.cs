using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BlogTag> BlogTags { get; set; }
        public List<CourseTag> CourseTags { get; set; }
        public List<TutorialPostTag> TutorialPostTags { get; set; }
    }
}
