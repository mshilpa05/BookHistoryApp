namespace Application.Models
{
    public class ChangeHistoryParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public uint StartYear { get; set; }
        public uint EndYear { get; set; } = (uint)DateTime.Now.Year;
    }
}
