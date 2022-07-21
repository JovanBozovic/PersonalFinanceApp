namespace PersonalFinanceApp.Database.Entities
{
    public class CategoryEntity
    {

        public int code { get; set; }
        public int parent_code { get; set; }
        public string name { get; set; }

    }
}
