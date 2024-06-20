using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MineBlog.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "URL Slug is required")]
        public string UrlSlug { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Url(ErrorMessage = "Invalid URL")]
        public string ImageUrl { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        // Foreign keys
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        // Navigation properties
        public Category Category { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public int AuthorId { get; set; }

        public Account Author { get; set; }
    }
}
