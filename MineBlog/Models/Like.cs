namespace MineBlog.Models
{
    public class Like
    {
        public int Id { get; set; }

        // Foreign keys
        public int BlogId { get; set; }
        public int AccountId { get; set; }

        // Navigation properties
        public Blog Blog { get; set; }
        public Account Account { get; set; }
    }
}
