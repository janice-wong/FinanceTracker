import { Expense, ExpenseData, ExpenseMonth } from './types';
import moment from 'moment';
import axios from 'axios';

const expensesUrl = "https://localhost:5001/expenses";

export const loadExpenses = () => {
  return axios.get(expensesUrl).then((response) => response.data);
};

export const loadImportedFile = () => {
  const url = `${expensesUrl}/fileUpload`;
  return axios.get(url).then((response) => response.data);
};

export const uploadFile = (file: File) => {
  const formData = new FormData();
  formData.set("file", file);

  const url = `${expensesUrl}/fileUpload`;
  axios.post(url, formData, { headers: { 'Content-Type': 'multipart/form-data' } });
};

export const updateTypes = (expenses: ExpenseData[]) =>
  expenses.map(expense => ({...expense, transactionDate: moment(expense.transactionDate)}));

export const getMonthsForExpenses = (expenses: Expense[]): Set<string> => {
  const expenseMonths = expenses.map(expense => {
    const transactionDate = new Date(expense.transactionDate);
    return ({
      month: transactionDate.getMonth(),
      year: transactionDate.getFullYear()
    } as ExpenseMonth);
  });
  return new Set<string>(expenseMonths.map(expenseMonth => `${moment().month(expenseMonth.month).format("MMM")} ${expenseMonth.year}`));
};

export const getExpenseMonth = (monthString: string): ExpenseMonth =>
  ({
    month: getMonthIndexedAtOne(moment().month(monthString.slice(0, 3)).toDate()),
    year: parseInt(monthString.slice(4))
  } as ExpenseMonth);

export const filterExpensesByMonth = (expenseMonth: ExpenseMonth, expenses: Expense[]): Expense[] =>
  expenses.filter(expense => {
    const transactionDate = new Date(expense.transactionDate);
    return getMonthIndexedAtOne(transactionDate) === expenseMonth.month &&
      transactionDate.getFullYear() === expenseMonth.year;
  });

const getMonthIndexedAtOne = (transactionDate: Date): number => transactionDate.getMonth() + 1;
