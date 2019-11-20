using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.Models
{
    public class ImageFile : BaseModel
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
    }
}
