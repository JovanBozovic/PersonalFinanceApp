using PersonalFinanceApp.Database.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceApp.Commands
{
    public class CreateCategoryCommand
    {
        [Required]
        public int code { get; set; }
        public int parrent_code { get; set; }
        [Required]
        public string name { get; set; }

    }
}
