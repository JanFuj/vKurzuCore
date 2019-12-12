using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vKurzuCore.Models;
using vKurzuCore.ViewModels.Admin;
using vKurzuCore.ViewModels.Dto;

namespace vKurzuCore.Data
{
    public class vKurzuDbContext : IdentityDbContext
    {
        public vKurzuDbContext(DbContextOptions<vKurzuDbContext> options)
            : base(options)
        {
        }
        public DbSet<AdminNote> AdminNotes { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Svg> Svgs { get; set; }
        public DbSet<ImageFile> ImageFiles { get; set; }
        public DbSet<TutorialPost> TutorialPosts { get; set; }
        public DbSet<TutorialCategory> TutorialCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //blog tags
            builder.Entity<BlogTag>()
                .HasKey(bt => new { bt.BlogId, bt.TagId });
            builder.Entity<BlogTag>()
                .HasOne(bt => bt.Blog)
                .WithMany(b => b.BlogTags)
                .HasForeignKey(bt => bt.BlogId);
            builder.Entity<BlogTag>()
                .HasOne(bt => bt.Tag)
                .WithMany(b => b.BlogTags)
                .HasForeignKey(bt => bt.TagId);

            //course tags
            builder.Entity<CourseTag>()
             .HasKey(ct => new { ct.CourseId, ct.TagId });
            builder.Entity<CourseTag>()
                .HasOne(ct => ct.Course)
                .WithMany(b => b.CourseTags)
                .HasForeignKey(ct => ct.CourseId);
            builder.Entity<CourseTag>()
                .HasOne(bt => bt.Tag)
                .WithMany(b => b.CourseTags)
                .HasForeignKey(bt => bt.TagId);

            //tutorial post tags
            builder.Entity<TutorialPostTag>()
           .HasKey(tt => new { tt.TutorialPostId, tt.TagId });
            builder.Entity<TutorialPostTag>()
                .HasOne(ct => ct.Post)
                .WithMany(b => b.TutorialPostTags)
                .HasForeignKey(ct => ct.TutorialPostId);
            builder.Entity<TutorialPostTag>()
                .HasOne(bt => bt.Tag)
                .WithMany(b => b.TutorialPostTags)
                .HasForeignKey(bt => bt.TagId);

            builder.Entity<Svg>()
                .HasIndex(b => b.Name)
                .IsUnique();

            builder.Entity<Tag>()
                .HasIndex(b => b.Name)
                .IsUnique();

            builder.Entity<Course>()
              .HasIndex(b => b.UrlTitle)
              .IsUnique();

            builder.Entity<Blog>()
              .HasIndex(b => b.UrlTitle)
              .IsUnique();

        }       

    }
}
