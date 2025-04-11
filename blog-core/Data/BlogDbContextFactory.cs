using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace blog_core.Data
{
    public class BlogDbContextFactory: IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Replace with your actual connection string
            // var connectionString = "Server=localhost, 1433;Database=BlogDB;User Id=sa;Password=Aye5089mere@;MultipleActiveResultSets=true;TrustServerCertificate=true";

            optionsBuilder.UseSqlServer(connectionString);

            return new BlogDbContext(optionsBuilder.Options);
        }
    }
}