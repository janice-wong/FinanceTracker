using System.Collections.Generic;
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

        public async Task DeleteExpenses() => await _expenseDataService.DeleteAll();
    }
}
