using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MineBlog.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Url(ErrorMessage = "Invalid URL")]
        public string ImageUrl { get; set; }

        // Navigation properties
        public ICollection<Blog> Blogs { get; set; }
    }
}
