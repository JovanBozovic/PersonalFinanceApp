
#nullable disable

using CsvHelper.Configuration;
using PersonalFinanceApp.Database.Entities;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Database.ClassMaps
{
    public class RulesClassMap : ClassMap<RuleEntity>
    {
        public RulesClassMap(){
        Map(m=>m.predicate).Name("predicate");
        Map(m=>m.catcode).Name("catcode");
        }

    }
}
