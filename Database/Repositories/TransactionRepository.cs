#nullable disable

using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Commands;
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
                if (!_dbContext.Transactions.Any(o => o.Id == transaction.Id))
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



        public async Task<PagedSortedList<TransactionEntity>> ListTransactions(int page = 1, int pageSize = 5, string sortBy = null, SortingOrder sortOrder = SortingOrder.Asc, List<string> transaction_kinds = null, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            var query = _dbContext.Transactions.Include(q=>q.SplitTransactions).AsQueryable();

            // var query= query2;
            if (StartDate != DateTime.MinValue)
            {
                query = query.Where(q => q.Date >= StartDate);
            };
            Console.WriteLine("TRANSATION KINDS=====" + StartDate);
            if (EndDate != DateTime.MinValue)
            {
                query = query.Where(q => q.Date <= EndDate);
            }
            Console.WriteLine("TRANSATION KINDS=====" + EndDate);
            if (transaction_kinds.Any())
            {
                query = query.Where(q => transaction_kinds.Contains(q.Kind));
            }
            Console.WriteLine("TRANSATION KINDS=====" + transaction_kinds);

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
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Beneficiary_Name) : query.OrderByDescending(x => x.Beneficiary_Name);
                        break;
                    case "mcc":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Mcc) : query.OrderByDescending(x => x.Mcc);
                        break;
                    case "currency":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Currency) : query.OrderByDescending(x => x.Currency);
                        break;
                    case "kind":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Kind) : query.OrderByDescending(x => x.Kind);
                        break;
                    case "direction":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Direction) : query.OrderByDescending(x => x.Direction);
                        break;
                    case "amount":
                        query = sortOrder == SortingOrder.Asc ? query.OrderBy(x => x.Amount) : query.OrderByDescending(x => x.Amount);
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

        public async Task<TransactionEntity> Categorize(TransactionEntity transaction, string Catcode)
        {
            if (_dbContext.Transactions.Any(o => o.Id == transaction.Id) && _dbContext.Categories.Any(o => o.code == Catcode))
            {
                // var categorizedTransaction=transaction;
                // categorizedTransaction.Catcode=Catcode;
                // _dbContext.Transactions.Add(categorizedTransaction);
                transaction.Catcode = Catcode;
            }


            await _dbContext.SaveChangesAsync();

            return transaction;
        }

        public async Task<CategorySpendingList> GetAnalytics(DateTime startDate, DateTime endDate, string direction, string Catcode)
        {
            var categoriesQuery = _dbContext.Categories.Include(cat => cat.transactions).AsQueryable();

            if (Catcode != null)
            {
                categoriesQuery = categoriesQuery.Where(x => x.code == Catcode);
            }

            var categories = await categoriesQuery.ToListAsync();

            var CategorySpendings = new CategorySpendingList();

            int count = 0;

            foreach (var category in categories)
            {
                var categoryList = await _dbContext.Categories.Include(c => c.transactions).Where(c => c.parent_code == category.code || c.code == category.code).ToListAsync();

                var amount = 0.0;

                foreach (var cat in categoryList)
                {

                    var transactions = _dbContext.Transactions.Where(t => t.Catcode == cat.code);
                    count = transactions.Count();
                    if (direction != null)
                    {
                        transactions = transactions.Where(t => t.Direction == direction);
                    }

                    if (!(startDate == DateTime.MinValue))
                    {

                        transactions = transactions.Where(t => t.Date >= startDate);
                    }
                    if (!(endDate == DateTime.MinValue))
                    {
                        transactions = transactions.Where(t => t.Date <= endDate);
                    }

                    amount += transactions.Select(t => t.Amount).Sum() + transactions.Select(t => t.Amount).Sum();
                }


                CategorySpendings.groups.Add(new CategorySpending { Catcode = category.code, Amount = amount, Count = count });
            }
            return CategorySpendings;
        }

        public async Task<bool> SplitTransaction(string Id, SplitTransactionCommand splitTransactionCommand)
        {
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var query = _dbContext.Transactions.Include(t => t.SplitTransactions).AsNoTracking().AsQueryable();

            var categoryQuery = _dbContext.Categories.AsQueryable().AsNoTracking();
            var intId=Int32.Parse(Id);
            var transaction = query.Where(t => t.Id == intId).FirstOrDefault();
            // var modId=Id+splitTransactionCommand.splits.First().Catcode;


            if (transaction == null)
            {
                return false;
            }


            if (transaction.SplitTransactions.Any())
            {
                _dbContext.SplittedTransactions.RemoveRange(transaction.SplitTransactions);
                transaction.SplitTransactions=null;
                await _dbContext.SaveChangesAsync();
            }


            
            transaction.Catcode="Z";  
            double SplittedTransactionsAmount = 0;
            List<SplitTransactionEntity> SplittedTransactionsList=new List<SplitTransactionEntity>{};
            foreach (var splitTransaction in splitTransactionCommand.splits)
            {
                SplittedTransactionsAmount += splitTransaction.Amount;
                if (categoryQuery.Where(t => t.code == splitTransaction.Catcode) != null)
                {
                    SplittedTransactionsList.Add(new SplitTransactionEntity
                    {
                        Id = Id+splitTransaction.Catcode,
                        Catcode = splitTransaction.Catcode,
                        Amount = splitTransaction.Amount
                    });
                    // await _dbContext.AddAsync(new SplitTransactionEntity
                    // {
                    //     Id = Id,
                    //     Catcode = splitTransaction.Catcode,
                    //     Amount = splitTransaction.Amount
                    // });
                }else{
                    return false;
                }
            }
            if (transaction.Amount == SplittedTransactionsAmount)
            {
                foreach(var SplitTransaction in SplittedTransactionsList){
                    _dbContext.Add(SplitTransaction);
                    await _dbContext.SaveChangesAsync();
                }
                transaction.SplitTransactions=SplittedTransactionsList;
                await _dbContext.SaveChangesAsync();


                return true;
            }else{
                return false;
            }

        }
    }
}
