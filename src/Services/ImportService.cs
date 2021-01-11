using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsvHelper;
using FinanceTracker.Enums;
using FinanceTracker.Mappers;
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

        public async Task<Result<string>> ImportFile(IFormFile file) =>
            await Result.Try(
                async () =>
                {
                    using var reader = new StreamReader(file.OpenReadStream());
                    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture)
                    {
                        Configuration =
                        {
                            PrepareHeaderForMatch = (header, index) => Regex.Replace(header, " ", string.Empty),
                            IgnoreBlankLines = true,
                            HasHeaderRecord = true,
                            HeaderValidated = null,
                            MissingFieldFound = null,
                            ShouldSkipRecord = record => record.All(string.IsNullOrEmpty)
                        }
                    };

                    var expenses = new List<Expense>();
                    await csv.ReadAsync();
                    csv.ReadHeader();
                    while (await csv.ReadAsync())
                    {
                        expenses.Add(new Expense(
                            csv.GetField<DateTime>("TransactionDate"),
                            csv.GetField<DateTime>("PostDate"),
                            csv.GetField<string>("Description"),
                            csv.GetField<string>("Category").MapToExpenseCategory(),
                            csv.GetField<ExpenseType>("Type"),
                            csv.GetField<decimal>("Amount")));
                    }

                    await _expenseDataService.Create(expenses);

                    return file.FileName;
                },
                exception => throw new ApplicationException(exception.Message));
    }
}
