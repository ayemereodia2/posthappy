using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog_core.Dto.ResponseDto
{
    public class BlogResponseDto:BaseResponseDto
    {
        public int Id { get; set; }  // Unique identifier
        public string? Title { get; set; }  // Title of the blog post
        public string? Content { get; set; }  // Main content of the blog
        public string? HeaderImageUrl { get; set; }  // URL to the header image (optional)
        public DateTime DatePosted { get; set; }
        public string AuthorName { get; set; }
    }
}