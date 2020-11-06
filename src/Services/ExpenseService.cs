using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinanceTracker.Models;

namespace FinanceTracker.Services
{
    public class ExpenseService
    {
        private readonly ExpenseDataService _expenseDataService;

        public ExpenseService(ExpenseDataService expenseDataService)
        {
            _expenseDataService = expenseDataService;
        }

        public async Task<Result<List<Expense>>> ListExpenses() => await _expenseDataService.List();

        public Result<List<Expense>> SearchExpenses(IReadOnlyCollection<Expense> expenses, string descriptionToSearch) =>
            Result.Try(
                () => PrepareSearch(expenses).Where(e => e.Description.Contains(descriptionToSearch)).ToList(),
                exception => throw new ApplicationException(exception.Message));

        private static IReadOnlyCollection<Expense> PrepareSearch(IEnumerable<Expense> expenses) =>
            expenses.Select(e =>
            {
                e.Description = e.Description.ToLower();
                return e;
            }).ToList().AsReadOnly();
    }
}
