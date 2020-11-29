using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinanceTracker.Models;

namespace FinanceTracker.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<Result<List<Expense>>> ListExpenses();
    }
}
