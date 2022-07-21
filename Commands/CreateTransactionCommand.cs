using PersonalFinanceApp.Database.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceApp.Commands
{
    public class CreateTransactionCommand
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Beneficiary_Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Direction { get; set; }
        [Required]
        public float Amount { get; set; }

        public string Description { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public int Mcc { get; set; }

        public string Kind { get; set; }
        [ForeignKey("code")]
        public int catcode {get;set;}
    }
}
