namespace Application.Models
{
    public class ChangeHistoryGroupedByBookId
    {
        public string BookId { get; set; } = string.Empty;

        public int ChangeHistoryLogCount { get; set; }
    }
}
