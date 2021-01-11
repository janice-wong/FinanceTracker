import React, { Component } from 'react'
import { Switch, Route, BrowserRouter } from 'react-router-dom'

import { Home } from 'src/components/Home'
import { AppHeader } from 'src/components/AppHeader'
import { ExpensesTable } from 'src/components/ExpensesTable'
import { Months } from 'src/components/Months'
import { MonthlyExpenses } from 'src/components/MonthlyExpenses'
import { loadExpenses, convertTransactionDateToMoment } from './operations'
import { AppState } from './types';
import './App.css';

class App extends Component {
  state: AppState = {
    expenses: []
  };

  componentDidMount = async () => {
    const data = await loadExpenses().then(convertTransactionDateToMoment);
    this.setState({ expenses: data });
  };

  render() {
    return (
      <BrowserRouter forceRefresh>
        <div className="container app">
          <AppHeader />

          <Switch>
            <Route path="/" exact render={() => <Home expenses={this.state.expenses} />} />
            <Route path="/expenses" exact render={() => <ExpensesTable expenses={this.state.expenses} />} />
            <Route path="/months"  exact render={() => <Months expenses={this.state.expenses} />} />
            <Route path="/monthlyExpenses/:month/:year" exact render={({ match }) => <MonthlyExpenses expenses={this.state.expenses} match={match} />} />
          </Switch>
        </div>
      </BrowserRouter>
    );
  }
}

export default App;
