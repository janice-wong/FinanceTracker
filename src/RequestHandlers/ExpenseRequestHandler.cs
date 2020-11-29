using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinanceTracker.Extensions;
using FinanceTracker.Grpc;
using FinanceTracker.Mappers;
using FinanceTracker.Services.Interfaces;
using Grpc.Core;
using UploadStatusCode = FinanceTracker.Grpc.UploadFileResponse.Types.UploadStatusCode;

namespace FinanceTracker.RequestHandlers
{
    public class ExpenseRequestHandler : IExpenseRequestHandler
    {
        private readonly IExpenseService _expenseService;
        private readonly IImportService _importService;

        public ExpenseRequestHandler(IExpenseService expenseService, IImportService importService)
        {
            _expenseService = expenseService;
            _importService = importService;
        }

        public async Task<ListExpensesResponse> ListExpenses() =>
            await _expenseService.ListExpenses()
                .Tap(_ => Console.WriteLine("Expenses listed"))
                .Map(expenses => expenses.Select(expense => expense.ToProto()))
                .Map(expenses => new ListExpensesResponse { Expenses = { expenses } })
                .UnwrapOrThrow("Unable to list expenses");

        public async Task<UploadFileResponse> ImportExpenses(IAsyncStreamReader<UploadFileRequest> requestStream)
        {
            var successfulUpload = new UploadFileResponse { StatusCode = UploadStatusCode.Ok };
            await foreach (var uploadFileRequest in requestStream.ReadAllAsync())
            {
                var result = await _importService.ImportExpenses(uploadFileRequest.Content.ToByteArray());
                if (result.IsFailure)
                {
                    return new UploadFileResponse { StatusCode = UploadStatusCode.Failed };
                }
            }

            return successfulUpload;
        }
    }
}
