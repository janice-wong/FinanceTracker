using FinanceTracker.Enums;

namespace FinanceTracker.Mappers
{
    public static class ExpenseCategoryMapper
    {
        public static ExpenseCategory MapToExpenseCategory(this string expenseCategory)
        {
            switch (expenseCategory)
            {
                case nameof(ExpenseCategory.Entertainment):
                    return ExpenseCategory.Entertainment;
                case nameof(ExpenseCategory.FoodAndDrink):
                case "Food & Drink":
                    return ExpenseCategory.FoodAndDrink;
                case nameof(ExpenseCategory.Groceries):
                    return ExpenseCategory.Groceries;
                case nameof(ExpenseCategory.HealthAndWellness):
                case "Health & Wellness":
                    return ExpenseCategory.HealthAndWellness;
                case nameof(ExpenseCategory.Travel):
                    return ExpenseCategory.Travel;
                case nameof(ExpenseCategory.FeesAndAdjustments):
                case "Fees & Adjustments":
                    return ExpenseCategory.FeesAndAdjustments;
                case nameof(ExpenseCategory.Shopping):
                    return ExpenseCategory.Shopping;
                case nameof(ExpenseCategory.Gas):
                    return ExpenseCategory.Gas;
                default:
                    return ExpenseCategory.Misc;
            }
        }
    }
}
