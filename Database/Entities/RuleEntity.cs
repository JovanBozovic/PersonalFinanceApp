#nullable disable

namespace PersonalFinanceApp.Database.Entities
{
    public class RuleEntity
    {
        public int Id;
        public string predicate { get; set; }
        public string catcode { get; set; }

    }
}
