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
using FinanceTracker.Services.Interfaces;

namespace FinanceTracker.Services.Implementations
{
    public class ImportService : IImportService
    {
        private readonly IExpenseDataService _expenseDataService;

        public ImportService(IExpenseDataService expenseDataService)
        {
            _expenseDataService = expenseDataService;
        }

        public async Task<Result> ImportExpenses(byte[] fileContent) =>
            await Result.Try(
                async () =>
                {
                    await using var memoryStream = new MemoryStream(fileContent);
                    using var streamReader = new StreamReader(memoryStream);
                    using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture)
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

                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await csvReader.ReadAsync();
                    csvReader.ReadHeader();

                    while (await csvReader.ReadAsync())
                    {
                        expenses.Add(new Expense(
                            csvReader.GetField<DateTime>("TransactionDate"),
                            csvReader.GetField<DateTime>("PostDate"),
                            csvReader.GetField<string>("Description"),
                            csvReader.GetField<string>("Category").MapToExpenseCategory(),
                            csvReader.GetField<ExpenseType>("Type"),
                            csvReader.GetField<double>("Amount")));
                    }

                    await _expenseDataService.Create(expenses);
                },
                exception => throw new ApplicationException(exception.Message));
    }
}
