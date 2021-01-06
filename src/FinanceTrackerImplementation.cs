using System.Threading.Tasks;
using FinanceTracker.Grpc;
using FinanceTracker.RequestHandlers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace FinanceTracker
{
    public class FinanceTrackerImplementation : Grpc.FinanceTracker.FinanceTrackerBase
    {
        private readonly IExpenseRequestHandler _expenseRequestHandler;

        public FinanceTrackerImplementation(IExpenseRequestHandler expenseRequestHandler)
        {
            _expenseRequestHandler = expenseRequestHandler;
        }

        public override Task<ListExpensesResponse> ListExpenses(Empty request, ServerCallContext context) =>
            _expenseRequestHandler.ListExpenses();

        // public override Task<UploadFileResponse> UploadFile(
        //     IAsyncStreamReader<UploadFileRequest> requestStream,
        //     ServerCallContext context) =>
        //     _expenseRequestHandler.ImportExpenses(requestStream);
    }
}
