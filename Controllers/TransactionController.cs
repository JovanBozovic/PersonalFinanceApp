using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private static List<Models.Transaction> transactions = new List<Models.Transaction>
        {
            new Models.Transaction
            {
                Id=32134,
                Beneficiary_Name="Pera",
                Date=new DateTime(2015, 12, 25),
                Direction="d",
                Amount=123,
                Currency="USD",
                Mcc=4232,
                Kind="Asd"
                },
                new Models.Transaction
            {
                Id=22134,
                Beneficiary_Name="Mika",
                Date=new DateTime(2016, 12, 25),
                Direction="d",
                Amount=132,
                Currency="EUR",
                Mcc=4242,
                Kind="Asd"
                }
            };



        [HttpGet]
        public ActionResult<List<Models.Transaction>> Get()
        {
            return Ok(transactions);
        }

        [HttpGet("{Id}")]
        public ActionResult<Models.Transaction> Get(int Id)
        {
            var oneTransaction=transactions.Find(h=>h.Id==Id);
            if(oneTransaction==null){
                return BadRequest("Transaction not Found");
            }
            return Ok(oneTransaction);
        }

        [HttpPost]
        public ActionResult<List<Models.Transaction>> AddHere(Models.Transaction transaction)
        {   
            transactions.Add(transaction);
            return Ok(transactions);
        }
    }
}