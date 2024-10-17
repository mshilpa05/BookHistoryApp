namespace Application.DTOs
{
    public class BookViewDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Author { get; set; }
    }
}
