import React, { FC } from 'react';
import shortid from 'shortid';

import { ExpenseRow } from './ExpenseRow'
import { Expenses } from '../types';

export const ExpensesTable: FC<Expenses> = ({ expenses }) => {
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
