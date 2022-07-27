#nullable disable

using System.ComponentModel.DataAnnotations.Schema;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Database.Entities
{

    public class TransactionEntity
    {
        
        public string Id { get; set; }
        public string Beneficiary_Name { get; set; }

        public DateTime Date { get; set; }

        public string Direction { get; set; }

        public float Amount { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }

        public int? Mcc { get; set; }
        public String Kind { get; set; }
        [ForeignKey("code")]
        public string Catcode { get; set; }
        public CategoryEntity category { get; set; }
        public List<SplitTransactionEntity> SplitTransactions { get; set;}

    }
}
