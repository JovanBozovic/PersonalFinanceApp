using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PersonalFinanceApp.Commands;
using PersonalFinanceApp.Services;

namespace PersonalFinanceApp.Controllers
{

    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetTransactions([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string sortBy, [FromQuery] SortOrder sortOrder)
        // {
        //     page = page ?? 1;
        //     pageSize = pageSize ?? 10;
        //     _logger.LogInformation("Returning {page}. page of products", page);
        //     var result = await _transactionService.GetTransactions(page.Value, pageSize.Value, sortBy, sortOrder);
        //     return Ok(result);
        // }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int Id)
        {
            var transaction = await _transactionService.GetTransaction(Id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }
        
        [HttpPost("api/transactions/import")]
        public async Task<IActionResult> ImportTransactions(){
            var transactions = await _transactionService.ImportTransactions();
            // if (transactions == null)
            // {
            //     return BadRequest();
            // }
            return Ok(transactions);
        }

        [HttpPost("api/transactions/create")]
        public async Task<IActionResult> CreateTransaction(CreateTransactionCommand command)
        {
            var result = await _transactionService.CreateTransaction(command);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        // [HttpDelete("{productCode}")]
        // public async Task<IActionResult> DeleteProduct([FromRoute] string productCode)
        // {
        //     var result = await _productsService.DeleteProduct(productCode);
        //     if (!result)
        //     {
        //         return NotFound();
        //     }
        //     return Ok();
        // }
    }
}
