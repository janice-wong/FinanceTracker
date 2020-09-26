using System;

namespace FinanceTracker.Models
{
    public class Expense
    {
        public DateTime TransactionDate { get; set; }
        public DateTime PostDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}
