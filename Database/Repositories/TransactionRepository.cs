using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Database.Entities;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Database.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionsDbContext _dbContext;

        public TransactionRepository(TransactionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TransactionEntity> Create(TransactionEntity transaction)
        {
            _dbContext.Transactions.Add(transaction);

            await _dbContext.SaveChangesAsync();

            return transaction;
        }

        public async Task<bool> Delete(int Id)
        {
            var transaction = await Get(Id);

            if (transaction == null)
            {
                return false;
            }

            _dbContext.Remove(transaction);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<TransactionEntity> Get(int Id)
        {
            return await _dbContext.Transactions.FirstOrDefaultAsync(p => p.Id == Id);
        }

        // public async Task<PagedSortedList<ProductEntity>> List(int page = 1, int pageSize = 5, string sortBy = null, SortOrder sortOrder = SortOrder.Asc)
        // {
        //     var query = _dbContext.Products.AsQueryable();

        //     var totalCount = query.Count();

        //     var totalPages = (int)Math.Ceiling(totalCount * 1.0 / pageSize);

        //     if (!string.IsNullOrEmpty(sortBy))
        //     {
        //         switch (sortBy)
        //         {
        //             case "code":
        //                 query = sortOrder == SortOrder.Asc ? query.OrderBy(x => x.Code) : query.OrderByDescending(x => x.Code);
        //                 break;
        //             case "description":
        //                 query = sortOrder == SortOrder.Asc ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Description);
        //                 break;
        //             default:
        //             case "name":
        //                 query = sortOrder == SortOrder.Asc ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
        //                 break;
        //         }
        //     } 
        //     else
        //     {
        //         query = query.OrderBy(p => p.Name);
        //     }

        //     query = query.Skip((page - 1) * pageSize).Take(pageSize);

        //     var items = await query.ToListAsync();

        //     return new PagedSortedList<ProductEntity>
        //     {
        //         Page = page,
        //         PageSize = pageSize,
        //         TotalCount = totalCount,
        //         TotalPages = totalPages,
        //         Items = items,
        //         SortBy = sortBy,
        //         SortOrder = sortOrder
        //     };
        // }
    }
}
