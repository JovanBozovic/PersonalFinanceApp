using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Commands
{
    public class SplitTransactionCommand
    {
         public List<SplittedTransactions> splits { get; set; } = new List<SplittedTransactions>();
    }
}