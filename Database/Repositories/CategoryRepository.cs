using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Database.Entities;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Database.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TransactionsDbContext _dbContext;

        public CategoryRepository(TransactionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<CategoryEntity>> Import(List<CategoryEntity> Categories)
        {
            throw new NotImplementedException();
        }
    }
}
