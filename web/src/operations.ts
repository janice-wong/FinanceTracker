import { Expense, ExpenseData, ExpenseMonth, MonthlyExpenseTotal } from './types';
import moment from 'moment';
import axios from 'axios';
import { groupBy, sum } from 'ramda';

const expensesUrl = "http://localhost:50051/expenses";

export const loadExpenses = () => {
  return axios.get(expensesUrl).then((response) => response.data);
};

export const uploadFile = (file: File) => {
  const formData = new FormData();
  formData.set("file", file);

  const url = `${expensesUrl}/import`;
  axios.post(url, formData, { headers: { 'Content-Type': 'multipart/form-data' } });
};

export const updateTypes = (expenses: ExpenseData[]) =>
  expenses.map(expense => ({...expense, transactionDate: moment(expense.transactionDate)}));

export const getMonthlyTotals = (expenses: Expense[]): MonthlyExpenseTotal[] => {
  const monthlyExpenseTotals = [];
  const monthlyExpenses = groupBy((expense) => getExpenseMonthTextByDate(expense.transactionDate), expenses);
  for (const month in monthlyExpenses) {
    monthlyExpenseTotals.push({
      formattedExpenseMonth: month,
      total: sum(monthlyExpenses[month].map((expense) => expense.amount))
    } as MonthlyExpenseTotal);
  }
  return monthlyExpenseTotals;
};

export const getExpenseMonthTextByDate = (transactionDate: Date): string => {
  const date = new Date(transactionDate);
  return `${moment().month(date.getMonth()).format("MMM")} ${date.getFullYear()}`;
};

export const getExpenseMonth = (formattedExpenseMonth: string): ExpenseMonth =>
  ({
    month: getMonthIndexedAtOne(moment().month(formattedExpenseMonth.slice(0, 3)).toDate()),
    year: parseInt(formattedExpenseMonth.slice(4))
  } as ExpenseMonth);

export const filterExpensesByMonth = (expenseMonth: ExpenseMonth, expenses: Expense[]): Expense[] =>
  expenses.filter(expense => {
    const transactionDate = new Date(expense.transactionDate);
    return getMonthIndexedAtOne(transactionDate) === expenseMonth.month &&
      transactionDate.getFullYear() === expenseMonth.year;
  });

const getMonthIndexedAtOne = (transactionDate: Date): number => transactionDate.getMonth() + 1;

export const formatCurrency = (amount: Number) => amount.toFixed(2);

export const getCurrencyStyle = (amount: Number) => amount < 0 ? { color: 'red' } : { color: 'black' };
