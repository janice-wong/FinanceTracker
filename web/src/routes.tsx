import React, { FC } from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';

import { ExpensesTable } from './components/ExpensesTable'
import { Months } from './components/Months'
import { MonthlyExpenses } from './components/MonthlyExpenses'
import { Expenses } from './types';
import { Home } from './components/Home';

export const Routes: FC<Expenses> = ({ expenses }) => (
  <Router>
    {/*<div>*/}
    {/*  <Switch>*/}
    {/*    <Route path="/">*/}
    {/*      <Home />*/}
    {/*    </Route>*/}
    {/*    <Route path="/expenses">*/}
    {/*      <ExpensesTable expenses={expenses} />*/}
    {/*    </Route>*/}
    {/*    <Route path="/months">*/}
    {/*      <Months expenses={expenses} />*/}
    {/*    </Route>*/}
    {/*    <Route path="/monthlyExpenses/:month/:year">*/}
    {/*      <MonthlyExpenses expenses={expenses} />*/}
    {/*    </Route>*/}
    {/*  </Switch>*/}
    {/*</div>*/}
  </Router>
);
