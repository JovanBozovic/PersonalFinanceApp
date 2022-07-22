#nullable disable

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
        public async Task<List<TransactionEntity>> Import(List<TransactionEntity> transactions)
        {

            foreach (var transaction in transactions)
            {
                if(!_dbContext.Transactions.Any(o=>o.Id==transaction.Id))
                _dbContext.Transactions.Add(transaction);
            }
            await _dbContext.SaveChangesAsync();

            return transactions;
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



        public async Task<PagedSortedList<TransactionEntity>> ListTransactions(int page = 1, int pageSize = 5, string sortBy = null, SortingOrder sortOrder = SortingOrder.Asc,List<string> transaction_kinds=null ,DateTime? StartDate=null,DateTime? EndDate=null)
        {
            var query = _dbContext.Transactions.AsQueryable();

            // var query= query2;
            if(StartDate!=DateTime.MinValue){
                query=query.Where(q=>q.Date>=StartDate);
            };
            Console.WriteLine("TRANSATION KINDS====="+StartDate);
            if(EndDate!=DateTime.MinValue){
                query=query.Where(q=>q.Date<=EndDate);
            }
            Console.WriteLine("TRANSATION KINDS====="+EndDate);
            if(transaction_kinds.Any()){
                query=query.Where(q=>transaction_kinds.Contains(q.Kind));
            }
            Console.WriteLine("TRANSATION KINDS====="+transaction_kinds);

            var totalCount = query.Count();

            var totalPages = (int)Math.Ceiling(totalCount * 1.0 / pageSize);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "date":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Date) : query.OrderByDescending(x => x.Date);
                        break;
                    case "description":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Description);
                        break;
                    case "beneficiary_name":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Beneficiary_Name);
                        break;
                    case "mcc":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Mcc);
                        break;
                    case "currency":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Currency);
                        break;
                    case "kind":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Kind);
                        break;
                    case "direction":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Direction);
                        break;
                    case "amount":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Amount);
                        break;
                    default:
                    case "id":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);
                        break;
                }
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var items = await query.ToListAsync();

            return new PagedSortedList<TransactionEntity>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Items = items,
                SortBy = sortBy,
                SortingOrder = sortOrder
            };
        }

        public async Task<TransactionEntity> Categorize(TransactionEntity transaction,string Catcode)
        {
            if(_dbContext.Transactions.Any(o=>o.Id==transaction.Id))
            {
                var categorizedTransaction=transaction;
                categorizedTransaction.Catcode=Catcode;
                _dbContext.Transactions.Add(categorizedTransaction);
            }
             

            await _dbContext.SaveChangesAsync();

            return transaction;
        }
    }
}
