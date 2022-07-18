using PersonalFinanceApp.Database.Entities;

namespace PersonalFinanceApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public String Beneficiary_Name  { get; set; }

        public DateTime Date { get; set; }

        public String Direction { get; set; }

        public float Amount { get; set; }

        public String Description { get; set; }

        public String Currency { get; set; }

        public int Mcc { get; set; }

        public String Kind { get; set; }
    }
}