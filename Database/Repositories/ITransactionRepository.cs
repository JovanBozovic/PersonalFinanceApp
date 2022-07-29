#nullable disable

using PersonalFinanceApp.Commands;
using PersonalFinanceApp.Database.Entities;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Database.Repositories
{
    public interface ITransactionRepository
    {
        Task<TransactionEntity> Create(TransactionEntity transaction);

        Task<TransactionEntity> Get(int Id);
        Task<List<TransactionEntity>> Import(List<TransactionEntity> transactions);

        Task<PagedSortedList<TransactionEntity>> ListTransactions(int page = 1, int pageSize = 5, string sortBy = null, SortingOrder sortOrder = SortingOrder.Asc,List<string> transaction_kinds=null,DateTime? StartDate=null,DateTime? EndDate=null);
        Task<bool> Delete(int Id);
        Task<TransactionEntity> Categorize(TransactionEntity transaction,string Catcode);
        Task<CategorySpendingList> GetAnalytics(DateTime startDate, DateTime endDate, string direction=null, string Catcode=null);
        Task<bool> SplitTransaction(string Id, SplitTransactionCommand splitTransactionCommand);
        Task AutoCategorize();

        Task<List<RuleEntity>> ImportRules(List<RuleEntity> rules);
        void ExportTransactions();
    }
}
