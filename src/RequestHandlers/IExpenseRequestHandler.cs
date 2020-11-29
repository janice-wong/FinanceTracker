using System.Threading.Tasks;
using FinanceTracker.Grpc;
using Grpc.Core;

namespace FinanceTracker.RequestHandlers
{
    public interface IExpenseRequestHandler
    {
        Task<ListExpensesResponse> ListExpenses();

        Task<UploadFileResponse> ImportExpenses(IAsyncStreamReader<UploadFileRequest> requestStream);
    }
}
