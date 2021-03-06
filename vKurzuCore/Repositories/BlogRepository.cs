﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Data;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public vKurzuDbContext vKurzuDbContext
        {
            get { return Context as vKurzuDbContext; }

        }
        public BlogRepository(vKurzuDbContext context) : base(context)
        {

        }
        public IEnumerable<Blog> GetFirst3BlogPosts()
        {
            return vKurzuDbContext.Blogs.Include(b => b.BlogTags).Where(b => b.Approved && !b.Deleted).OrderBy(c => c.Position).Take(3).ToList();
        }

        public override Task<Blog> FindByIdAsync(int id)
        {
            return vKurzuDbContext.Blogs.Include("BlogTags.Tag")
                 .SingleOrDefaultAsync(x => x.Id == id && !x.Deleted);
        }

        public Task<Blog> FindByUrlAsync(string urlTitle)
        {
            return vKurzuDbContext.Blogs.Include("BlogTags.Tag").FirstOrDefaultAsync(c => !c.Deleted && c.Approved && c.UrlTitle == urlTitle);
        }

        public Task<Blog> FindPreviewBlogByUrlAsync(string urlTitle)
        {
            return vKurzuDbContext.Blogs.Include("BlogTags.Tag").FirstOrDefaultAsync(c => !c.Deleted && c.UrlTitle == urlTitle);
        }
        public IEnumerable<Blog> GetPublishedBlogArticles()
        {
            return vKurzuDbContext.Blogs.Include("BlogTags.Tag").Where(c => !c.Deleted && c.Approved).OrderBy(c => c.Position).ToList();
        }

        public IEnumerable<Blog> GetRelatedArticles(int id)
        {
            return GetPublishedBlogArticles().Where(x => x.Id != id).Take(3);
        }
    }
}
