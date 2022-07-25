#nullable disable

namespace PersonalFinanceApp.Models
{
    public class SplitTransaction
    {
        public int Id { get; set; }

        public string Catcode { get; set; }

        public double Amount { get; set; }

        public Transaction Transaction { get; set; }

        public Category Category { get; set; }

    }
}