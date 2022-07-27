#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceApp.Database.Entities
{
    public class SplitTransactionEntity
    {
        
        public string Id { get; set; }
        [ForeignKey("Id")]
        public string Transaction_Id {get;set;}

        public string Catcode { get; set; }

        public double Amount { get; set; }

        public TransactionEntity Transaction { get; set; }

        public CategoryEntity Category { get; set; }

    }
}