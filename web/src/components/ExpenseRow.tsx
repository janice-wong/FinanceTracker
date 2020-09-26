import React, { FC } from 'react';
import moment from 'moment';
import { Expense } from '../types';

export const ExpenseRow: FC<Expense> = ({
  transactionDate,
  description,
  category,
  type,
  amount
}) => (
    <tr>
      <td>{moment(transactionDate).format('MMM DD')}</td>
      <td>{description}</td>
      <td>{category}</td>
      <td>{type}</td>
      <td align='right' style={amount < 0 ? { color: 'red' } : { color: 'black' }}>{amount}</td>
    </tr>
);
