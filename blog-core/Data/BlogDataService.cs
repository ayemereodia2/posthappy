using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog_core.entities;
using blog_core.interfaces;

namespace blog_core.Data
{
    public class BlogDataService : IBlogDataService
{
    private readonly IRepository<Blog> _blogRepository;

    public BlogDataService(IRepository<Blog> blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task AddBlog(Blog blog)
    {
        await _blogRepository.AddAsync(blog); // Ensure to await it
    }

    public async Task<IEnumerable<Blog>> GetBlogs()
    {
        return await _blogRepository.GetAllAsync(); // Ensure method names match
    }

    public async Task<Blog?> GetBlogById(int id)
    {
        return await _blogRepository.GetByIdAsync(id); // Ensure to return something
    }

    public async Task UpdateBlog(int id, Blog blog)
    {
        await _blogRepository.UpdateAsync(id, blog); // Ensure to await it
    }

    public async Task DeleteBlog(int id)
    {
        await _blogRepository.DeleteAsync(id); // Ensure to await it
    }
}
}