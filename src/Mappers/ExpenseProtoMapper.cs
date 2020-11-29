using System;
using FinanceTracker.Enums;
using Google.Type;
using DateTime = System.DateTime;
using ExpenseProto = FinanceTracker.Grpc.Expense;
using ExpenseDomain = FinanceTracker.Models.Expense;
using ExpenseCategoryProto = FinanceTracker.Grpc.Expense.Types.ExpenseCategory;
using ExpenseCategoryDomain = FinanceTracker.Enums.ExpenseCategory;
using ExpenseTypeProto = FinanceTracker.Grpc.Expense.Types.ExpenseType;
using ExpenseTypeDomain = FinanceTracker.Enums.ExpenseType;

namespace FinanceTracker.Mappers
{
    public static class ExpenseProtoMapper
    {
        public static ExpenseProto ToProto(this ExpenseDomain expenseDomain) =>
            new ExpenseProto
            {
                TransactionDate = expenseDomain.TransactionDate.ToDate(),
                PostDate = expenseDomain.TransactionDate.ToDate(),
                Description = expenseDomain.Description,
                ExpenseCategory = expenseDomain.Category.ToProto(),
                ExpenseType = expenseDomain.Type.ToProto(),
                Amount = expenseDomain.Amount
            };

        private static ExpenseCategoryProto ToProto(this ExpenseCategoryDomain expenseCategoryDomain) =>
            expenseCategoryDomain switch
            {
                ExpenseCategoryDomain.FoodAndDrink => ExpenseCategoryProto.FoodAndDrink,
                ExpenseCategoryDomain.Groceries => ExpenseCategoryProto.Groceries,
                ExpenseCategoryDomain.Shopping => ExpenseCategoryProto.Shopping,
                ExpenseCategoryDomain.HealthAndWellness => ExpenseCategoryProto.HealthAndWellness,
                ExpenseCategoryDomain.Travel => ExpenseCategoryProto.Travel,
                ExpenseCategoryDomain.FeesAndAdjustments => ExpenseCategoryProto.FeesAndAdjustments,
                ExpenseCategoryDomain.Gas => ExpenseCategoryProto.Gas,
                ExpenseCategoryDomain.Entertainment => ExpenseCategoryProto.Entertainment,
                ExpenseCategoryDomain.Misc => ExpenseCategoryProto.Misc,
                ExpenseCategory.Undefined => throw new ArgumentOutOfRangeException(),
                _ => throw new ArgumentOutOfRangeException()
            };

        private static ExpenseTypeProto ToProto(this ExpenseTypeDomain expenseType) =>
            expenseType switch
            {
                ExpenseTypeDomain.Sale => ExpenseTypeProto.Sale,
                ExpenseTypeDomain.Adjustment => ExpenseTypeProto.Adjustment,
                ExpenseTypeDomain.Return => ExpenseTypeProto.Return,
                ExpenseTypeDomain.Payment => ExpenseTypeProto.Payment,
                ExpenseTypeDomain.Undefined => throw new ArgumentOutOfRangeException(),
                _ => throw new ArgumentOutOfRangeException()
            };

        private static Date ToDate(this DateTime dateTime) =>
            new Date
            {
                Month = dateTime.Month,
                Day = dateTime.Day,
                Year = dateTime.Year
            };
    }
}
