using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog_core.entities;

namespace blog_core.interfaces
{
    public interface IBlogService 
{
    Task CreateBlog(Blog blog);
    Task<IEnumerable<Blog>> GetAllBlogs();
    Task<Blog?> GetBlogById(int id);
    Task DeleteBlog(int id);

    /*
    Task UpdateBlog(int id, Blog blog);
    IEnumerable<Blog> GetBlogsByCategory(string category);
    IEnumerable<Blog> GetBlogsByTags(List<string> tags);
    string GetBlogSlugById(int id);
    Blog GetBlogBySlug(string slug); 
    void PublishBlog(int id);
    void UnpublishBlog(int id);
    IEnumerable<Blog> SearchBlogs(string searchTerm);*/
}
}