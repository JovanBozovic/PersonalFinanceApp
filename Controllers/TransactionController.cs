#nullable disable

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PersonalFinanceApp.Commands;
using PersonalFinanceApp.Services;
using PersonalFinanceApp.Models;

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

        [HttpGet("api/transactions")]
        public async Task<IActionResult> GetTransactions([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string sortBy, [FromQuery] SortingOrder sortingOrder, [FromQuery] List<string> transaction_kinds, [FromQuery] DateTime StartDate, [FromQuery] DateTime EndDate)
        {
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            _logger.LogInformation("Returning {page}. page of products", page);
            var result = await _transactionService.GetTransactions(page.Value, pageSize.Value, sortBy, sortingOrder, transaction_kinds, StartDate, EndDate);
            return Ok(result);
        }

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
        public async Task<IActionResult> ImportTransactions()
        {
            var transactions = await _transactionService.ImportTransactions();
            // if (transactions == null)
            // {
            //     return BadRequest();
            // }
            return Ok(transactions);
        }

        [HttpPost("api/transactions/{Id}/categorize")]
        public async Task<IActionResult> CategorizeTransaction([FromRoute] int Id, [FromQuery] string Catcode)
        {
            var result = await _transactionService.CategorizeTransaction(Id, Catcode);
            return Ok(result);
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
        [HttpGet("api/spending-analytics")]
        public async Task<ActionResult<CategorySpendingList>> GetAnalytics([FromQuery] DateTime startDate,[FromQuery] DateTime endDate,[FromQuery] string Catcode=null,[FromQuery] string direction=null)
        {
            var result = await _transactionService.GetAnalytics(startDate, endDate, direction, Catcode);
            return Ok(result);
        }

        [HttpPost("api/transactions/{Id}/split")]
        public async Task<ActionResult>SplitTransaction([FromRoute] string Id, [FromBody] SplitTransactionCommand splitTransactionCommand)
        {
            var result=await _transactionService.SplitTransaction(Id, splitTransactionCommand);

            return Ok(result);
        }
        [HttpPost("api/transactions/auto-categorize")]
        public async Task<ActionResult>AutoCategorizeTransactions()
        {
            var result=await _transactionService.AutoCategorize();
            return Ok(result);
        }

    }
}
