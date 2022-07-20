using PersonalFinanceApp.Database.Entities;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Database.Repositories
{
    public interface ITransactionRepository
    {        
        Task<TransactionEntity> Create(TransactionEntity transaction);

        Task<TransactionEntity> Get(int Id);
       Task<List<TransactionEntity>> Import(List<TransactionEntity> transactions);

        Task<bool> Delete(int Id);
    }
}
