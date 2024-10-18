namespace Domain.Entities
{
    public class Book
    {
        public string Id { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public DateTime PublishDate { get; set; }

        private readonly List<ChangeHistory> _changeHistories = new List<ChangeHistory>();
        public IReadOnlyList<ChangeHistory> ChangeHistories => _changeHistories.AsReadOnly();

        public void Update(string? title, string? author, string? description, DateTime publishDate)
        {
            if (Title != title)
            {
                LogChange($"Title was changed from \"{Title}\" to \"{title}\" ");
                Title = title;
            }

            if (Author != author)
            {
                LogChange($"Author was changed from \"{Author}\" to \"{author}\"");
                Author = author;
            }

            if (Description != description)
            {
                LogChange($"Description was changed from \"{Description}\" to \"{description}\"");
                Author = author;
            }

            if (PublishDate != publishDate)
            {
                LogChange($"PublishDate was changed from \"{PublishDate}\" to \"{publishDate}\"");
            }
        }

        private void LogChange(string description)
        {
            var changeHistory = new ChangeHistory(Id, description);
            _changeHistories.Add(changeHistory);
        }
    }

    public class ChangeHistory
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public string ChangeDescription { get; set; }
        public DateTime ChangeDate { get; set; }

        public ChangeHistory(string bookId, string changeDescription)
        {
            BookId = bookId;
            ChangeDescription = changeDescription;
            ChangeDate = DateTime.UtcNow;
        }
    }

}
