#nullable disable

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

        public async Task<CategoryEntity> Get(string code)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(p => p.code == code);
        }
        public async Task<List<CategoryEntity>> Import(List<CategoryEntity> Categories)
        {
            foreach (var category in Categories)
            {
                if (_dbContext.Categories.Any(o => o.code == category.code) /*|| _dbContext.Categories.Any(o=>o.parent_code==category)*/)
                {
                    var existingCategory = _dbContext.Categories.FirstOrDefault(o => o.code == category.code);
                    existingCategory.name = category.name;
                    // _dbContext.Categories.

                }
                else _dbContext.Categories.Add(category);

            }
            // _dbContext.Categories.AddRange(Categories);
            await _dbContext.SaveChangesAsync();

            return Categories;
        }
    }
}
