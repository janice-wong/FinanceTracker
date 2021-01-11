import axios from 'axios';

const origin = "http://localhost:8080";

export const expensesPath = `${origin}/expenses`;
export const importExpensesPath = `${origin}/expenses/import`;

export class Api {
  static listExpenses() {
    return axios.get(expensesPath);
  }

  static importExpenses(formData: FormData) {
    return axios.post(importExpensesPath, formData, { headers: { 'Content-Type': 'multipart/form-data' } });
  }
}
