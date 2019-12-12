using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Helpers.Contracts;
using vKurzuCore.Models;
using vKurzuCore.Repositories;

namespace vKurzuCore.Helpers
{
    public class TagParser : ITagParser
    {
        private readonly IUnitOfWork _unitOfWork;
        public TagParser(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Finds tag in db according to given string
        /// </summary>
        public async Task<List<int>> ParseTags(string tagy)
        {
            var existingTagIds = new List<int>();
            if (string.IsNullOrEmpty(tagy)) return existingTagIds;

            var tagIds = tagy.Trim().Split('&');

            foreach (var tagId in tagIds)
            {
                if (string.IsNullOrWhiteSpace(tagId)) continue;
                var existingTag = await _unitOfWork.Tags.FindByIdAsync(int.Parse(tagId.Trim()));
                if (existingTag == null) continue;
                existingTagIds.Add(existingTag.Id);
            }

            return existingTagIds;
        }
    }
}
