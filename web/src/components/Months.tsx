import React, { FC } from 'react';
import { Expenses } from '../types';
import {
  formatCurrency,
  getCurrencyStyle,
  getExpenseMonth,
  getMonthlyTotals,
} from '../operations';
import { Link } from 'react-router-dom';
import './Months.css';

export const Months: FC<Expenses> = ({
  expenses
}) => {
  const monthlyTotals = getMonthlyTotals(expenses);

  return (
    <div>
      <h2>Which month do you want to see expenses for?</h2>
      <div className="list-group">
        {
          Array.from(monthlyTotals).map(monthlyTotal =>
          {
            const expenseMonth = getExpenseMonth(monthlyTotal.formattedExpenseMonth);
            return (
              <Link
                to={`/monthlyExpenses/${expenseMonth.month}/${expenseMonth.year}`}
                key={`/monthlyExpenses/${expenseMonth.month}/${expenseMonth.year}`}
                className="list-group-item list-group-item-action"
              >
                <div className="inline">
                  <div>{monthlyTotal.formattedExpenseMonth}</div>
                  <div style={getCurrencyStyle(monthlyTotal.total)}>{formatCurrency(monthlyTotal.total)}</div>
                </div>
              </Link>
            );
          })
        }
      </div>
    </div>
  );
}
