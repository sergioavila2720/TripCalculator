import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { FetchTrips } from './components/FetchTrips';
import { AddTrip } from './components/AddTrip';
import { FetchExpenses } from './components/FetchExpenses';
import { AddExpenses } from './components/AddExpenses';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
        <Route path='/fetch-trips' component={FetchTrips} />
        <Route path='/addtrip' component={AddTrip} />
        <Route path='/trips/edit/:tripId' component={AddTrip} />
        <Route path='/expensesbytrips/:tripId' component={FetchExpenses} />
        <Route path='/addexpenses/:tripId' component={AddExpenses} />
      </Layout>
    );
  }
}
