using PersonalFinanceApp.Commands;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Services
{
    public interface ITransactionService
    {
        // Task<PagedSortedList<Models.Transactions>> GetProducts(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc);
        Task<Models.Transaction> GetTransaction(int Id);
        Task<Models.Transaction> CreateTransaction(CreateTransactionCommand command);
        Task<bool> DeleteTransaction(int Id);
    }
}
