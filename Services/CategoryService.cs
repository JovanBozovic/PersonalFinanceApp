#nullable disable

using AutoMapper;
using CsvHelper;
using PersonalFinanceApp.Commands;
using PersonalFinanceApp.Database.ClassMaps;
using PersonalFinanceApp.Database.Entities;
using PersonalFinanceApp.Database.Repositories;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        

        public async Task<bool> ImportCategories()
        {

            using (var streamReader = new StreamReader(@"C:\Users\Instructor\Desktop\Projekat\PersonalFinanceApp\PersonalFinanceApp\Files\categories.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, System.Globalization.CultureInfo.InvariantCulture))
                {
                    // csvReader.Configuration.HeaderValidated=null;
                    csvReader.Context.RegisterClassMap<CategoryClassMap>();
                    var categoryRecords = csvReader.GetRecords<CategoryEntity>().ToList();
                    Console.WriteLine(categoryRecords);
                    var categoryEntity = await _categoryRepository.Import(categoryRecords);
                    return true;
                }

            }

        }


        // public async Task<Models.Transaction> CreateTransaction(CreateTransactionCommand command)
        // {
        //     var entity = _mapper.Map<TransactionEntity>(command);

        //     var existingTransaction = await _transactionRepository.Get(command.Id);
        //     if (existingTransaction != null)
        //     {
        //         return null;
        //     }
        //     var result = await _transactionRepository.Create(entity);

        //     return _mapper.Map<Models.Transaction>(result);
        // }



        // public async Task<bool> DeleteProduct(int Id)
        // {
        //     return await _transactionRepository.Delete(Id);
        // }

        // public Task<bool> DeleteTransaction(int Id)
        // {
        //     throw new NotImplementedException();
        // }

        public async Task<Models.Category> GetCategories(string code)
        {
            var categoryEntity = await _categoryRepository.Get(code);

            if (categoryEntity == null)
            {
                return null;
            }

            return _mapper.Map<Models.Category>(categoryEntity);
        }



        //     public async Task<PagedSortedList<Models.Transaction>> GetTransactions(int page = 1, int pageSize = 10, string sortBy = null, SortingOrder sortOrder = SortingOrder.Asc,List<string> transaction_kinds=null,DateTime? StartDate=null,DateTime? EndDate=null)
        //     {
        //         var result = await _transactionRepository.ListTransactions(page, pageSize, sortBy, sortOrder,transaction_kinds,StartDate,EndDate);

        //         return _mapper.Map<PagedSortedList<Models.Transaction>>(result);
        //     }

    }
}