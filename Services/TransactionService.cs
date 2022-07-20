using AutoMapper;
using CsvHelper;
using PersonalFinanceApp.Commands;
using PersonalFinanceApp.Database.ClassMaps;
using PersonalFinanceApp.Database.Entities;
using PersonalFinanceApp.Database.Repositories;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }
        public async Task<Models.Transaction> CreateTransaction(CreateTransactionCommand command)
        {
            var entity = _mapper.Map<TransactionEntity>(command);

            var existingTransaction = await _transactionRepository.Get(command.Id);
            if (existingTransaction != null)
            {
                return null;
            }
            var result = await _transactionRepository.Create(entity);

            return _mapper.Map<Models.Transaction>(result);
        }



        public async Task<bool> DeleteProduct(int Id)
        {
            return await _transactionRepository.Delete(Id);
        }

        public Task<bool> DeleteTransaction(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Transaction> GetTransaction(int Id)
        {
            var transactionEntity = await _transactionRepository.Get(Id);

            if (transactionEntity == null)
            {
                return null;
            }

            return _mapper.Map<Models.Transaction>(transactionEntity);
        }

        public async Task<bool> ImportTransactions()
        {

            using (var streamReader = new StreamReader(@"C:\Users\Instructor\Desktop\Projekat\PersonalFinanceApp\PersonalFinanceApp\Files\transactions.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, System.Globalization.CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<TransactionClassMap>();
                    var transactionRecords = csvReader.GetRecords<TransactionEntity>().ToList();
                    Console.WriteLine(transactionRecords);
                    var transactionEntity = await _transactionRepository.Import(transactionRecords);
                    return true;
                }

            }

        }


        //     public async Task<PagedSortedList<Models.Product>> GetProducts(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc)
        //     {
        //         var result = await _productsRepository.List(page, pageSize, sortBy, sortOrder);

        //         return _mapper.Map<PagedSortedList<Models.Product>>(result);
        //     }
        // }
    }
}