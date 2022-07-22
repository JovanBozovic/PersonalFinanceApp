#nullable disable

using PersonalFinanceApp.Commands;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Services
{
    public interface ICategoryService
    {
        // Task<PagedSortedList<Models.Transaction>> GetTransactions(int page = 1, int pageSize = 10, string sortBy = null, SortingOrder sortOrder = SortingOrder.Asc,List<string> transaction_kinds=null,DateTime? StartDate=null,DateTime? EndDate=null);
        Task<Models.Category> GetCategories(string code);
        Task<bool> ImportCategories();
        // Task<Models.Transaction> CreateTransaction(CreateTransactionCommand command);
        // Task<bool> DeleteTransaction(int Id);
    }
}
