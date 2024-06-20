using System.Collections.Generic;

namespace MineBlog.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation properties
        public ICollection<Blog> Blogs { get; set; }
    }
}
