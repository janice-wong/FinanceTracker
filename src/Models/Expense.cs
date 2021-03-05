using System;
using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;
using FinanceTracker.Enums;

namespace FinanceTracker.Models
{
    public class Expense
    {
        public Guid Id { get; }
        public DateTime TransactionDate { get; }
        public DateTime PostDate { get; }
        public string Description { get; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExpenseCategory Category { get; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExpenseType Type { get; }
        public decimal Amount { get; }

        [Ignore]
        public DateTime ImportDate { get; set; }

        public Expense() { }

        public Expense(
            DateTime transactionDate,
            DateTime postDate,
            string description,
            ExpenseCategory category,
            ExpenseType type,
            decimal amount)
        {
            TransactionDate = transactionDate;
            PostDate = postDate;
            Description = description;
            Category = category;
            Type = type;
            Amount = amount;
            ImportDate = DateTime.UtcNow;
        }
    }
}
