import { RouteComponentProps } from 'react-router-dom';

export interface AppState {
  expenses: Expense[]
}

export interface FileUploadProps extends RouteComponentProps { }

export interface FileUploadState {
  selectedFile: File,
  error: string
}

export interface FileUploadResult {
  success: boolean
}

export interface HomeProps {
  expenses: Expense[]
}

export interface MonthlyExpensesProps extends Partial<RouteComponentProps<MonthParams>> {
  expenses: Expense[]
}

export interface MonthParams {
  month: string,
  year: string
}

export interface ExpenseData {
  amount: number,
  category: string,
  description: string,
  transactionDate: string,
  type: string
}

export interface Expense {
  amount: number,
  category: string,
  description: string,
  transactionDate: Date,
  type: string
}

export interface Expenses {
  expenses: Expense[]
}

export interface ExpenseMonth {
  month: number,
  year: number
}

export interface MonthlyExpenseTotal {
  formattedExpenseMonth: string,
  total: number
}
