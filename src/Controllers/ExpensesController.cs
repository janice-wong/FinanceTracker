using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinanceTracker.Models;
using FinanceTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseDataService _expenseDataService;
        private readonly ExpenseService _expenseService;

        public ExpensesController(ExpenseDataService expenseDataService, ExpenseService expenseService)
        {
            _expenseDataService = expenseDataService;
            _expenseService = expenseService;
        }

        [HttpGet]
        public IReadOnlyCollection<Expense> List() => GetExpenses().Value;

        [HttpGet("fileUpload")]
        public string GetUploadedFileName() => _expenseDataService.GetImportedFileName().Value;

        [HttpPost("fileUpload")]
        public async Task<IActionResult> Post(IFormFile file) =>
            await _expenseDataService.ImportFile(file)
                .OnFailure(errorMessage => throw new ApplicationException(errorMessage))
                .Finally(_ => Ok());

        [HttpGet("search/{searchTerm}")]
        public IReadOnlyCollection<Expense> Search(string searchTerm) =>
            GetExpenses()
                .Bind(expenses => _expenseService.SearchExpenses(expenses, searchTerm))
                .OnFailure(errorMessage => Console.WriteLine($"Search failed due to: {errorMessage}"))
                .Tap(_ => Console.WriteLine("Search was successful."))
                .Value;

        private Result<ReadOnlyCollection<Expense>> GetExpenses() =>
            _expenseDataService.ReadExpensesFromCsv()
                .Ensure(expenses => expenses.Any(), "There are no expenses imported.")
                .Tap(_ => Console.WriteLine("Obtained expenses!"));
    }
}
