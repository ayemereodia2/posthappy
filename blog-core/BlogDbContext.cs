using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using blog_core.entities;

namespace blog_core
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options): base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
    }

}