#nullable disable

using PersonalFinanceApp.Database.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceApp.Commands
{
    public class CreateCategoryCommand
    {
        [Required]
        public string code { get; set; }
        public string parent_code { get; set; }
        [Required]
        public string name { get; set; }

    }
}
