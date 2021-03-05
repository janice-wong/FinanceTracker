import React, { FC } from 'react';
import { ExpenseMonth, MonthlyExpensesProps } from '../types';
import { filterExpensesByMonth } from '../operations';
import ExpensesTable from './ExpensesTable';

export const MonthlyExpenses: FC<MonthlyExpensesProps> = ({
  expenses,
  match
}) => {
  const monthToFilter = { month: parseInt(match.params.month), year: parseInt(match.params.year) } as ExpenseMonth;
  const monthlyExpenses = filterExpensesByMonth(monthToFilter, expenses);

  return (
    <ExpensesTable expenses={monthlyExpenses} />
  );
};
