using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinanceTracker.Models;
using FinanceTracker.Services.Interfaces;

namespace FinanceTracker.Services.Implementations
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseDataService _expenseDataService;

        public ExpenseService(IExpenseDataService expenseDataService)
        {
            _expenseDataService = expenseDataService;
        }

        public Task<Result<List<Expense>>> ListExpenses() => _expenseDataService.List();
    }
}
