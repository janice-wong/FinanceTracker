using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsvHelper;
using FinanceTracker.Enums;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Http;

namespace FinanceTracker.Services
{
    public class ImportService
    {
        private readonly ExpenseDataService _expenseDataService;

        public ImportService(ExpenseDataService expenseDataService)
        {
            _expenseDataService = expenseDataService;
        }

        public async Task<Result> ImportFile(IFormFile file) =>
            await Result.Try(
                async () =>
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                        csvReader.Configuration.PrepareHeaderForMatch =
                            (header, index) => Regex.Replace(header, " ", string.Empty);
                        csvReader.Configuration.HeaderValidated = null;
                        csvReader.Configuration.MissingFieldFound = null;

                        var expenses = csvReader.GetRecords<Expense>().Where(e => e.Type != ExpenseType.Payment).ToList();
                        await _expenseDataService.Create(expenses);
                    }
                },
                exception => throw new ApplicationException(exception.Message));
    }
}
