namespace Domain.Entities
{
    public class Book
    {
        public string Id { get; set; } = String.Empty;
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
