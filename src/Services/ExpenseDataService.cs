using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Dapper;
using FinanceTracker.Database;
using FinanceTracker.Models;
using Npgsql;

namespace FinanceTracker.Services
{
    public class ExpenseDataService
    {
        public async Task<Result<List<Expense>>> List()
        {
            var sql = $@"
                SELECT
                    transaction_date AS {nameof(Expense.TransactionDate)},
                    post_date AS {nameof(Expense.PostDate)},
                    description AS {nameof(Expense.Description)},
                    amount AS {nameof(Expense.Amount)},
                    expense_category AS {nameof(Expense.Category)},
                    expense_type AS {nameof(Expense.Type)},
                    import_date AS {nameof(Expense.ImportDate)}
                FROM expense;";

            return await Result.Try(
                async () => (await ExecuteWithNewConnection(connection => connection.QueryAsync<Expense>(sql)))
                    .ToList(),
                exception => throw new Exception(exception.Message));
        }

        public async Task Create(List<Expense> expenses)
        {
            var sql = $@"
                INSERT INTO expense (
                    transaction_date,
                    post_date,
                    description,
                    amount,
                    expense_category,
                    expense_type,
                    import_date)
                VALUES (
                    @{nameof(Expense.TransactionDate)},
                    @{nameof(Expense.PostDate)},
                    @{nameof(Expense.Description)},
                    @{nameof(Expense.Amount)},
                    @{nameof(Expense.Category)}::expense_category,
                    @{nameof(Expense.Type)}::expense_type,
                    @{nameof(Expense.ImportDate)});";

            await Result.Try(
                async () =>
                {
                    foreach (var expense in expenses)
                    {
                        var parameters = new
                        {
                            expense.TransactionDate,
                            expense.PostDate,
                            expense.Description,
                            expense.Amount,
                            Category = expense.Category.ToString(),
                            Type = expense.Type.ToString(),
                            expense.ImportDate
                        };

                        await ExecuteWithNewConnection(async connection =>
                            await connection.ExecuteAsync(sql, parameters));
                    }
                },
                exception => throw new Exception(exception.Message));
        }

        public async Task DeleteAll()
        {
            var sql = "DELETE FROM expense;";
            await Result.Try(
                async () => await ExecuteWithNewConnection(connection => connection.ExecuteAsync(sql)),
                exception => throw new Exception(exception.Message));
        }

        private async Task<T> ExecuteWithNewConnection<T>(Func<IDbConnection, Task<T>> dataAccessMethod)
        {
            await using var connection = new NpgsqlConnection(DatabaseConfig.GetConnectionString());
            return await dataAccessMethod(connection);
        }
    }
}
