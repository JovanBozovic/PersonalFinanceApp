#nullable disable

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

            CreateMap<CategoryEntity, Models.Category>()
               .ForMember(d => d.code, opts => opts.MapFrom(s => s.code));

            CreateMap<SplitTransactionEntity,Models.SplitTransaction>()
            .ForMember(d=>d.Id,opts=>opts.MapFrom(s=>s.Id));
            // CreateMap<Models.SplitTransaction,SplitTransactionEntity>()
            // .ForMember(d=>d.Id,opts=>opts.MapFrom(s=>s.Id));


            // CreateMap<List<Models.Transaction>,List<TransactionEntity>>()
            //     .ForMember(d => d["id"], opts => opts.MapFrom(s => s["id"]));

            CreateMap<PagedSortedList<TransactionEntity>, PagedSortedList<Models.Transaction>>();

            CreateMap<CreateTransactionCommand, TransactionEntity>()
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.Id));
        }
    }
}
