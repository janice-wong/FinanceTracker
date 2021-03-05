import React, { Component } from 'react';

import { ExpenseRow } from './ExpenseRow'
import { ExpenseCategory, Expenses, ExpensesTableState } from '../types';
import { convertToString, convertTransactionDateToMoment, loadExpenses } from '../operations';
import { MDBDropdown, MDBDropdownItem, MDBDropdownMenu, MDBDropdownToggle } from 'mdbreact';
import { uniq } from 'ramda';

class ExpensesTable extends Component<Expenses, ExpensesTableState> {
  constructor(props: Expenses) {
    super(props);

    this.state = {
      filter: ExpenseCategory.Undefined,
      filteredExpenses: []
    };
  }

  componentDidMount = async () => {
    const data = await loadExpenses().then(convertTransactionDateToMoment);
    this.setState({ filteredExpenses: data });
  };

  getDropdownCategories = (): ExpenseCategory[] =>
    uniq(
      this.props.expenses
        .map(expense => expense.category)
        .filter(category => category !== this.state.filter));

  handleFilter = (filter: ExpenseCategory) =>
    this.setState({
      filter,
      filteredExpenses: this.props.expenses.filter(expense => expense.category === filter)
    });

  removeFilter = () =>
    this.setState({
      filter: null,
      filteredExpenses: this.props.expenses
    });

  render() {
    return (
      <div>
        <MDBDropdown>
          <MDBDropdownToggle caret color="primary">
            { this.state.filter || 'Filter by Category' }
          </MDBDropdownToggle>
          <MDBDropdownMenu basic>
            { convertToString(this.state.filter) &&
             <MDBDropdownItem onClick={() => this.removeFilter()}>All</MDBDropdownItem> }
            { this.getDropdownCategories().map(category =>
              <MDBDropdownItem
                key={category}
                onClick={() => this.handleFilter(category)}
              >
                {convertToString(category)}
              </MDBDropdownItem>
            )}
          </MDBDropdownMenu>
        </MDBDropdown>
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
            this.state.filteredExpenses.map(expense => (
              <ExpenseRow
                id={expense.id}
                key={expense.id}
                transactionDate={expense.transactionDate}
                description={expense.description}
                category={expense.category}
                type={expense.type}
                amount={expense.amount} />
            ))
          }
          </tbody>
        </table>
      </div>
    );
  }
}

export default ExpensesTable;
