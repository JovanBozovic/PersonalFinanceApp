#nullable enable

namespace PersonalFinanceApp.Models
{
    public class AnalyticsPagedSortedList<T>
    {
        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public SortingOrder SortingOrder { get; set; }

        public string? SortBy { get; set; }

        public List<T>? Items { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? category { get; set; }
        public string? direction { get; set; }

    }
}
