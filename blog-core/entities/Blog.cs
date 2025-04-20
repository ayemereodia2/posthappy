using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog_core.entities
{
    public record Blog
    {
        public int Id { get; set; }  // Unique identifier
        public string? Title { get; set; }  // Title of the blog post
        public string? Content { get; set; }  // Main content of the blog
        public string? HeaderImageUrl { get; set; }  // URL to the header image (optional)
        public DateTime DatePosted { get; set; }  // Date the blog was posted
        public string? AuthorId { get; set; }  // Author's identifier (e.g., from user system)
        public ApplicationUser? Author { get; set; }
        public string? AuthorName  => $"{Author.FirstName} {Author.LastName}";
    }
}
/*public string? Category { get; set; }  // Category the blog belongs to (optional)
    public List<string>? Tags { get; set; }  // Tags associated with the blog post
    public bool IsPublished { get; set; }  // Flag for whether the blog is published
    public bool IsDeleted { get; set; }  // Flag for whether the blog is deleted (soft delete)
    public DateTime? LastModified { get; set; }  // Last modified date (optional)
    public string? Slug { get; set; }  // URL-friendly title*/