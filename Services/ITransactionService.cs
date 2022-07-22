#nullable disable

using PersonalFinanceApp.Commands;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Services
{
    public interface ITransactionService
    {
        Task<PagedSortedList<Models.Transaction>> GetTransactions(int page = 1, int pageSize = 10, string sortBy = null, SortingOrder sortOrder = SortingOrder.Asc,List<string> transaction_kinds=null,DateTime? StartDate=null,DateTime? EndDate=null);
        Task<Models.Transaction> GetTransaction(int Id);
        Task<bool> ImportTransactions();
        Task<Models.Transaction> CreateTransaction(CreateTransactionCommand command);
        Task<bool> DeleteTransaction(int Id);
        Task<Models.Transaction> CategorizeTransaction(int Id,string Catcode,CreateTransactionCommand command);
    }
}
