#nullable disable

namespace PersonalFinanceApp.Database.Entities
{
    public class CategoryEntity
    {
        public string code { get; set; }
        public string parent_code { get; set; }
        public string name { get; set; }
        public ICollection<TransactionEntity> transactions{get;set;}

    }
}
