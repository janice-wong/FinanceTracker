using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsvHelper;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Http;

namespace FinanceTracker.Services
{
    public class ExpenseDataService
    {
        private const string ImportPath = "src/Imports";

        public Result<ReadOnlyCollection<Expense>> ReadExpensesFromCsv() =>
            GetCsvReader()
                .Map(csvReader => csvReader.GetRecords<Expense>())
                .Bind(FilterExpenses)
                .Map(expenses => expenses.ToList().AsReadOnly());

        public Result<string> GetImportedFileName() =>
            GetImportedFiles()
                .Ensure(filePaths => filePaths.Count <= 1, "There can be at most one imported file.")
                .Map(filePaths => Path.GetFileName(filePaths.SingleOrDefault()));

        public async Task<Result> ImportFile(IFormFile file) =>
            await Result.Try(
                async () =>
                {
                    DeleteImportedFiles();

                    var filePath = $"{ImportPath}/{file.FileName}";
                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);
                },
                exception => throw new ApplicationException(exception.Message));

        private static Result<IEnumerable<Expense>> FilterExpenses(IEnumerable<Expense> expenses) =>
            Result.Try(() => expenses.Where(e => e.Type != "Payment"),
            exception => throw new ApplicationException(exception.Message));

        private static Result<CsvReader> GetCsvReader() =>
            GetStreamReader()
                // .Map(r => new CsvReader(r, CultureInfo.InvariantCulture));
                .Map(r =>
                {
                    var csv = new CsvReader(r, CultureInfo.InvariantCulture);
                    csv.Configuration.PrepareHeaderForMatch =
                        (header, index) => Regex.Replace(header, " ", string.Empty);

                    return csv;
                });

        private static Result<StreamReader> GetStreamReader() =>
            GetImportedFiles()
                .Ensure(filePaths => filePaths.Count == 1, "There should be one imported file.")
                .Map(filePaths => new StreamReader(filePaths.Single()));

        private static Result<List<string>> GetImportedFiles() =>
            Result.Try(
                () => Directory.GetFiles(ImportPath).ToList(),
                exception => throw new ApplicationException(exception.Message));

        private static void DeleteImportedFiles() => GetImportedFiles().Tap(filePaths => filePaths.ForEach(File.Delete));
    }
}
