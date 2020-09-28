import React, { FC } from 'react';
import shortid from 'shortid';
import { groupBy, prop } from 'ramda';

import { ExpenseRow } from './ExpenseRow'
import { Expenses } from '../types';
import { getMonthlyTotals } from '../operations';

export const ExpensesTable: FC<Expenses> = ({ expenses }) => {
  console.log(getMonthlyTotals(expenses))
const la = groupBy(prop('category'), expenses);
console.log(la);

  return (
    <div>
      <table className="table">
        <thead>
        <tr>
          <th scope="col">Transaction Date</th>
          <th scope="col">Description</th>
          <th scope="col">Category</th>
          <th scope="col">Type</th>
          <th scope="col">Amount</th>
        </tr>
        </thead>
        <tbody>
        {
          expenses.map(expense => (
            <ExpenseRow
              key={shortid.generate()}
              transactionDate={expense.transactionDate}
              description={expense.description}
              category={expense.category}
              type={expense.type}
              amount={expense.amount}
            />
          ))
        }
        </tbody>
      </table>
    </div>
  );
};
