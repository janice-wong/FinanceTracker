import axios from 'axios';

const origin = "http://localhost:8080";

export const listExpensesPath = `${origin}/expenses`;
export const importExpensesPath = `${origin}/expenses/import`;
export const deleteExpensesPath = `${origin}/expenses`;

export class Api {
  static listExpenses() {
    return axios.get(listExpensesPath);
  }

  static deleteExpenses() {
    return axios.delete(deleteExpensesPath);
  }

  static importExpenses(formData: FormData) {
    return axios.post(importExpensesPath, formData, { headers: { 'Content-Type': 'multipart/form-data' } });
  }
}
