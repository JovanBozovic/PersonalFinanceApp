#nullable disable

using PersonalFinanceApp.Database.Entities;

namespace PersonalFinanceApp.Models
{
   public class CategorySpending
    {
        public string Catcode { get; set; }

        public double Amount { get; set; }
        public int Count {get;set;}
    }
}