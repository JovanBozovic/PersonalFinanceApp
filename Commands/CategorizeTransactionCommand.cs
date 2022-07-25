#nullable disable

using PersonalFinanceApp.Database.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceApp.Commands
{
    public class CategorizeTransactionCommand
    {
        [Required]
        public int Id { get; set; }
        [ForeignKey("code")]
        public string Catcode { get; set; }
    }
}
