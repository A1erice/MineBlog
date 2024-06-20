using System;
using System.ComponentModel.DataAnnotations;

namespace MineBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [StringLength(500, ErrorMessage = "Content must be no longer than 500 characters")]
        public string Content { get; set; }

        // Foreign keys
        public int BlogId { get; set; }
        public int AccountId { get; set; }

        // Navigation properties
        public Blog Blog { get; set; }
        public Account Account { get; set; }
    }
}
