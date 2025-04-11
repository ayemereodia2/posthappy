using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog_core.entities;
using blog_core.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace my_blog_service.Controllers
{
    [Route("api/[controller]")] // Base route: /api/blog
    [ApiController] // Enables API-specific features like automatic model validation
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        // Constructor with dependency injection
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: api/blog
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetAllBlogs()
        {
            try
            {
                var blogs = await _blogService.GetAllBlogs();
                return Ok(blogs); // HTTP 200 with list of blogs
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using ILogger, not shown here)
                return StatusCode(500, "An error occurred while retrieving blogs: " + ex.Message);
            }
        }

        // GET: api/blog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlogById(int id)
        {
            try
            {
                var blog = await _blogService.GetBlogById(id);
                if (blog == null)
                {
                    return NotFound($"Blog with ID {id} not found"); // HTTP 404
                }
                return Ok(blog); // HTTP 200 with blog
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the blog: " + ex.Message);
            }
        }

        // POST: api/blog
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Blog>> CreateBlog([FromBody] Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400 if model validation fails
            }

            try
            {
                await _blogService.CreateBlog(blog);
                // Return 201 Created with the new resource's URI and the blog
                return CreatedAtAction(nameof(GetBlogById), new { id = blog.Id }, blog);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the blog: " + ex.Message);
            }
        }

        // DELETE: api/blog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            try
            {
                var blog = await _blogService.GetBlogById(id);
                if (blog == null)
                {
                    return NotFound($"Blog with ID {id} not found"); // HTTP 404
                }

                await _blogService.DeleteBlog(id);
                return NoContent(); // HTTP 204 for successful deletion
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the blog: " + ex.Message);
            }
        }

        // Optional: Health check or index endpoint
        [HttpGet("status")]
        public ActionResult Index()
        {
            return Ok("API is live"); // HTTP 200
        }
    }
}