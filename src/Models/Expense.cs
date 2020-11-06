using System;
using FinanceTracker.Enums;

namespace FinanceTracker.Models
{
    public class Expense
    {
        public DateTime TransactionDate { get; set; }
        public DateTime PostDate { get; set; }
        public string Description { get; set; }
        public ExpenseCategory Category { get; set; }
        public ExpenseType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime ImportDate { get; set; }

        public Expense() { }
    }
}
