
#nullable disable

using PersonalFinanceApp.Database.Entities;

namespace PersonalFinanceApp.Models
{
    public class CategorySpendingList
    {
        public List<CategorySpending> groups { get; set; } = new List<CategorySpending>();

    }

}