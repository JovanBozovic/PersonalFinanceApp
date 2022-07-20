using AutoMapper;
using PersonalFinanceApp.Commands;
using PersonalFinanceApp.Database.Entities;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TransactionEntity, Models.Transaction>()
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.Id));
            
            
            // CreateMap<List<Models.Transaction>,List<TransactionEntity>>()
            //     .ForMember(d => d["id"], opts => opts.MapFrom(s => s["id"]));

            // CreateMap<PagedSortedList<ProductEntity>, PagedSortedList<Models.Product>>();
            
            CreateMap<CreateTransactionCommand, TransactionEntity>()
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.Id));
        }
    }
}
