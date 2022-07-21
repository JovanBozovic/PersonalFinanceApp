using CsvHelper.Configuration;
using PersonalFinanceApp.Database.Entities;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Database.ClassMaps
{
    public class CategoryClassMap : ClassMap<CategoryEntity>
    {
        public CategoryClassMap(){
        // Map(m=>m.Id).Name("id");
        // Map(m=>m.Beneficiary_Name).Name("beneficiary-name");
        // Map(m=>m.Date).Name("date");
        // Map(m=>m.Direction).Name("direction");
        // Map(m=>m.Amount).Name("amount");
        // Map(m=>m.Description).Name("description");
        // Map(m=>m.Currency).Name("currency");
        // Map(m=>m.Mcc).Name("mcc");
        // Map(m=>m.Kind).Name("kind");
        // Map(m=>m.Catcode).Name("catcode");
    // id,beneficiary-name,date,direction,amount,description,currency,mcc,kind
        }

    }
}
