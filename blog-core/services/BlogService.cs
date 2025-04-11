using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog_core.Data;
using blog_core.entities;
using blog_core.interfaces;

namespace blog_core.services
{
    public class BlogService : IBlogService
{
    private IBlogDataService dataService;

    public BlogService(IBlogDataService dataService)
    {
        this.dataService = dataService;
    }

    public async Task CreateBlog(Blog blog)
    {
        await dataService.AddBlog(blog); // Ensure to await it
    }

    public async Task DeleteBlog(int id)
    {
        await dataService.DeleteBlog(id); // Ensure to await it
    }

    public async Task<IEnumerable<Blog>> GetAllBlogs()
    {
        return await dataService.GetBlogs(); // Ensure method names match
    }

    public async Task<Blog?> GetBlogById(int id)
    {
        return await dataService.GetBlogById(id); // Add return statement
    }

}

/*
/*public Blog GetBlogBySlug(string slug)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Blog> GetBlogsByCategory(string category)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Blog> GetBlogsByTags(List<string> tags)
    {
        throw new NotImplementedException();
    }

    public string GetBlogSlugById(int id)
    {
        throw new NotImplementedException();
    }

    public void PublishBlog(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Blog> SearchBlogs(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public void UnpublishBlog(int id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateBlog(int id, Blog blog)
    {
        await dataService.UpdateBlog(id, blog); // Ensure to await it
    }*/
}