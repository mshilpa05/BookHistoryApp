namespace Application.DTOs
{
    public class BookDTO
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Author { get; set; }
    }
}
