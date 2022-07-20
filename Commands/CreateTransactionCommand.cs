using PersonalFinanceApp.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceApp.Commands
{
    public class CreateTransactionCommand
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Beneficiary_Name { get; set; }

        public DateTime Date { get; set; }

        public string Direction { get; set; }

        public float Amount { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }

        public int Mcc { get; set; }

        public string Kind { get; set; }
    }
}
