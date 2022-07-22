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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<TransactionController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<TransactionController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpPost("api/categories/import")]
        public async Task<IActionResult> ImportCategories(){
            var categories = await _categoryService.ImportCategories();
            // if (transactions == null)
            // {
            //     return BadRequest();
            // }
            return Ok(categories);
        }

        // [HttpGet("api/transactions")]
        // public async Task<IActionResult> GetTransactions([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string sortBy, [FromQuery] SortingOrder sortingOrder,[FromQuery]List<string> transaction_kinds,[FromQuery] DateTime StartDate,[FromQuery] DateTime EndDate)
        // {
        //     page = page ?? 1;
        //     pageSize = pageSize ?? 10;
        //     _logger.LogInformation("Returning {page}. page of products", page);
        //     var result = await _transactionService.GetTransactions(page.Value, pageSize.Value, sortBy, sortingOrder,transaction_kinds,StartDate,EndDate);
        //     return Ok(result);
        // }

        [HttpGet("api/categories/{code}")]
        public async Task<IActionResult> GetCategories([FromRoute] string code)
        {
            var category = await _categoryService.GetCategories(code);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
        


        // [HttpPost("api/transactions/create")]
        // public async Task<IActionResult> CreateTransaction(CreateTransactionCommand command)
        // {
        //     var result = await _transactionService.CreateTransaction(command);
        //     if (result == null)
        //     {
        //         return BadRequest();
        //     }
        //     return Ok(result);
        // }

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
