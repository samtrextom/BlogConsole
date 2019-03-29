using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsConsole.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext() : base("name=BlogContext") { }

        public DbSet<Blog> Blog { get; set; }
        public DbSet<Post> Post { get; set; }

        public void AddBlog(Blog blog)
        {
            this.Blog.Add(blog);
            this.SaveChanges();
        }
    }
}
