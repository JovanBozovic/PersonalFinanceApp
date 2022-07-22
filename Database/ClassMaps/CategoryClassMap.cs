#nullable disable

using CsvHelper.Configuration;
using PersonalFinanceApp.Database.Entities;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Database.ClassMaps
{
    public class CategoryClassMap : ClassMap<CategoryEntity>
    {
        public CategoryClassMap(){
            Map(m=>m.code).Name("code");
            Map(m=>m.parent_code).Name("parent-code");
            Map(m=>m.name).Name("name");

        
        }

    }
}
