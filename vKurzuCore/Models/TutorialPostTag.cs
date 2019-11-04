using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.Models
{
    public class TutorialPostTag
    {
        public int TutorialPostId { get; set; }
        public TutorialPost Post { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
