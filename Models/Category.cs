#nullable disable

using PersonalFinanceApp.Database.Entities;

namespace PersonalFinanceApp.Models
{
    public class Category
    {
        public string code { get; set; }
        public string parent_code { get; set; }
        public string name { get; set; }

    }
}