using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace FinanceTracker.Services.Interfaces
{
    public interface IImportService
    {
        Task<Result> ImportExpenses(byte[] fileContent);
    }
}
