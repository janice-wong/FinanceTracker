using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinanceTracker.Models;

namespace FinanceTracker.Services.Interfaces
{
    public interface IExpenseDataService
    {
        Task<Result<List<Expense>>> List();
        Task Create(List<Expense> expenses);
        Task DeleteAll();
    }
}
