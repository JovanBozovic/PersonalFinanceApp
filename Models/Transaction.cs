#nullable disable

using PersonalFinanceApp.Database.Entities;

namespace PersonalFinanceApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public string Beneficiary_Name  { get; set; }

        public DateTime Date { get; set; }

        public string Direction { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }

        public int Mcc { get; set; }

        public string Kind { get; set; }
        public string Catcode {get;set;}
        public List<SplitTransactionEntity> SplitTransactions { get; set;}
    }
}