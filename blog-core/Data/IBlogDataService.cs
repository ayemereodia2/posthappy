using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog_core.entities;

namespace blog_core.Data
{
    public interface IBlogDataService
{
    Task AddBlog(Blog blog);
    Task<IEnumerable<Blog>> GetBlogs();
    Task<Blog?> GetBlogById(int id);
    Task UpdateBlog(int id, Blog blog);
    Task DeleteBlog(int id);
}
}