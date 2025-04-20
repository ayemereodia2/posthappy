using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using blog_core.Dto.RequestDto;
using blog_core.Dto.ResponseDto;
using blog_core.entities;
using blog_core.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace my_blog_service.Controllers
{
    [Route("api/[controller]")] // Base route: /api/blog
    [ApiController] // Enables API-specific features like automatic model validation
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly UserManager<ApplicationUser> _userManager;
        // Constructor with dependency injection
        public BlogController(
            IBlogService blogService,
            UserManager<ApplicationUser> userManager
            )
        {
            _blogService = blogService;
            _userManager = userManager;
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
        public async Task<ActionResult<BlogResponseDto>> GetBlogById(int id)
        {
            try
            {
                var blogTemp = await _blogService.GetBlogById(id);
                if (blogTemp == null)
                {
                    return NotFound($"Blog with ID {id} not found"); // HTTP 404
                }
                var blog = new BlogResponseDto 
                {
                    Id = blogTemp.Id,
                    Title = blogTemp.Title,
                    Content = blogTemp.Content,
                    HeaderImageUrl = blogTemp.HeaderImageUrl,
                    DatePosted = blogTemp.DatePosted,
                    AuthorName = blogTemp.AuthorName ?? "--"
                };
                return Ok(blog); // HTTP 200 with blog
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the blog: " + ex.Message);
            }
        }

        // POST: api/blog
        [Authorize]
        [HttpPost("createblog")]
        public async Task<ActionResult<Blog>> CreateBlog([FromBody] BlogRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400 if model validation fails
            }

            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userEmail != null)
                {
                    var user = await _userManager.FindByEmailAsync(userEmail);
                    if(user == null)
                    {
                        return Unauthorized(new BlogResponseDto{
                            Success = false,
                            Message = $"Unauthorized Access {userEmail}"
                        });
                    }

                    var blog = new Blog
                    {
                        Title = model.Title,
                        Content = model.Content,
                        DatePosted = model.DatePosted,
                        HeaderImageUrl = model.HeaderImageUrl,
                        Author = user
                    };


                    await _blogService.CreateBlog(blog);
                    // Return 201 Created with the new resource's URI and the blog
                    return CreatedAtAction(nameof(GetBlogById), new { id = blog.Id }, blog);
                }

                 return Unauthorized(new BlogResponseDto{
                            Success = false,
                            Message = $"Unauthorized: User ID does not exist {userEmail}"
                 });
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BlogResponseDto {
                    Success = false,
                    Message = "An error occurred while creating the blog: " + ex.Message
                });
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