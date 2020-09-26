import React, { FC } from 'react';
import { Expenses } from '../types';
import { getExpenseMonth, getMonthsForExpenses } from '../operations';
import { Link } from 'react-router-dom';

export const Months: FC<Expenses> = ({
  expenses
}) => {
  const months = getMonthsForExpenses(expenses);

  return (
    <div>
      <h2>Which month do you want to see expenses for?</h2>
      <div className="list-group">
        {
          Array.from(months).map(month =>
          {
            const expenseMonth = getExpenseMonth(month);
            return (
              <Link
                to={`/monthlyExpenses/${expenseMonth.month}/${expenseMonth.year}`}
                key={`/monthlyExpenses/${expenseMonth.month}/${expenseMonth.year}`}
                className="list-group-item list-group-item-action"
              >
                {month}
              </Link>
            );
          })
        }
      </div>
    </div>
  );
}
