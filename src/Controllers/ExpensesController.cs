using System;
using System.Collections.Generic;
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
        private readonly ImportService _importService;
        private readonly ExpenseService _expenseService;

        public ExpensesController(ImportService importService, ExpenseService expenseService)
        {
            _importService = importService;
            _expenseService = expenseService;
        }

        [HttpGet]
        public IReadOnlyCollection<Expense> List() => _expenseService.ListExpenses().Result.Value;

        [HttpPost("import")]
        public async Task<IActionResult> Post(IFormFile file) =>
            await _importService.ImportFile(file)
                .OnFailure(errorMessage => throw new ApplicationException(errorMessage))
                .Finally(_ => Ok());
    }
}
