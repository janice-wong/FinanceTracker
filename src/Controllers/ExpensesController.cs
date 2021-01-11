using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinanceTracker.Extensions;
using FinanceTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FinanceTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ImportService _importService;
        private readonly ExpenseService _expenseService;
        private readonly ILogger<ExpensesController> _logger;

        public ExpensesController(
            ImportService importService,
            ExpenseService expenseService,
            ILogger<ExpensesController> logger)
        {
            _importService = importService;
            _expenseService = expenseService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List() =>
            await _expenseService.ListExpenses()
                .Tap(_ => _logger.LogInformation("Expenses listed."))
                .UnwrapOrThrow("Unable to list expenses");

        [HttpPost("import")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            try
            {
                return await _importService.ImportFile(file)
                    .OnFailure(errorMessage => _logger.LogError(errorMessage))
                    .UnwrapOrThrow($"Unable to import file {file.FileName}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return BadRequestWithError(e);
            }
        }

        private IActionResult BadRequestWithError(Exception e) => BadRequest(new { Error = e.Message });
    }
}
