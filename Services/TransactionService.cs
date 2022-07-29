#nullable disable

using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
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
            // transactionEntity.SplitTransactions=

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
                    // csvReader.Configuration.HeaderValidated=null;
                    csvReader.Context.RegisterClassMap<TransactionClassMap>();
                    var transactionRecords = csvReader.GetRecords<TransactionEntity>().ToList();
                    Console.WriteLine(transactionRecords);
                    var transactionEntity = await _transactionRepository.Import(transactionRecords);
                    return true;
                }

            }

        }


        public async Task<PagedSortedList<Models.Transaction>> GetTransactions(int page = 1, int pageSize = 10, string sortBy = null, SortingOrder sortOrder = SortingOrder.Asc, List<string> transaction_kinds = null, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            var result = await _transactionRepository.ListTransactions(page, pageSize, sortBy, sortOrder, transaction_kinds, StartDate, EndDate);

            return _mapper.Map<PagedSortedList<Models.Transaction>>(result);
        }

        public async Task<Transaction> CategorizeTransaction(int Id, string Catcode)
        {
            // var entity = _mapper.Map<TransactionEntity>(Id);

            var existingTransaction = await _transactionRepository.Get(Id);
            if (existingTransaction == null)
            {
                return null;
            }
            var result = await _transactionRepository.Categorize(existingTransaction, Catcode);

            return _mapper.Map<Models.Transaction>(result);
        }

        public async Task<CategorySpendingList> GetAnalytics(DateTime startDate, DateTime endDate, string direction, string Catcode=null)
        {
            return await _transactionRepository.GetAnalytics(startDate, endDate, direction, Catcode);
        }

        public async Task<bool> SplitTransaction(string Id, SplitTransactionCommand command)
        {
            var result = await _transactionRepository.SplitTransaction(Id, command);
            
            return result;
        }

        public async Task<bool> AutoCategorize()
        {
            await ImportRules();
            await _transactionRepository.AutoCategorize();
            return true;
            
        }


        public async Task<bool> ImportRules()
        {

            using (var streamReader = new StreamReader(@"C:\Users\Instructor\Desktop\Projekat\PersonalFinanceApp\PersonalFinanceApp\Files\rules.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, System.Globalization.CultureInfo.InvariantCulture))
                {
                    // csvReader.Configuration.HeaderValidated=null;
                    csvReader.Context.RegisterClassMap<RulesClassMap>();
                    var ruleRecords = csvReader.GetRecords<RuleEntity>().ToList();
                    Console.WriteLine(ruleRecords);
                    var transactionEntity = await _transactionRepository.ImportRules(ruleRecords);
                    return true;
                }

            }

        }

        public void ExportTransactions()
        {
            _transactionRepository.ExportTransactions();
        }
    }
}