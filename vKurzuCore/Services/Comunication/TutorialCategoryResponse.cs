using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Models;

namespace vKurzuCore.Services.Comunication
{
    public class TutorialCategoryResponse : BaseResponse
    {
        public TutorialCategory Category { get; private set; }

        private TutorialCategoryResponse(bool success, string message, TutorialCategory category,string modelStateErrorKey) : base(success, message, modelStateErrorKey)
        {
            Category = category;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public TutorialCategoryResponse(TutorialCategory category) : this(true, string.Empty, category,"")
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public TutorialCategoryResponse(string message,string modelStateErrorKey) : this(false, message, null, modelStateErrorKey)
        { }
    }
}
