using PersonalFinanceApp.Database.Entities;

namespace PersonalFinanceApp.Models
{
    public class Category
    {
        public int code { get; set; }
        public int parent_code { get; set; }
        public string name { get; set; }

    }
}