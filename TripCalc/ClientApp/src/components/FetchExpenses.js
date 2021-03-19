import { extend } from 'jquery';
import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class Trip {
    constructor() {
        this.tripId = 0;
        this.tripName = "";
        this.tripCost = 0.0;
    }
}
export class FetchExpenses extends Component {
    constructor(props) {
        super(props);
        this.state = { trip: new Trip, expenses: [], loading: true }

        this.initialize();
    }

    async initialize() {
        var tripId = this.props.match.params["tripId"];
        const response = await fetch('api/tripsapi/' + tripId);
        const tripdata = await response.json();
        const expenseresponse = await fetch("api/expensesapi/tripid=" + tripId);
        const expensedata = await expenseresponse.json();


        this.setState({ trip: tripdata, expenses: expensedata, loading: false });
    }

    render() {
        let contents = this.state.loading ? <p><em>Loading...</em></p> : this.renderCreateForm(this.state.expenses);

        return (
        <div>
            <h1>Expenses made in {this.state.trip.TripName}</h1>
            <hr />                
            <div>                
                <p>This fetches expenses by trip from the server</p>
                    <p>
                        <Link to={'/addexpenses/' + this.state.trip.TripId}>Add Expense</Link>
                </p>
                </div>
                {contents}
            </div>
        )
    }

    renderCreateForm(expenses) {
        return (             

            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th></th>
                        <th>Name</th>
                        <th>Price</th>
                        <th>Student</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {expenses.map(expenses =>
                        <tr key={expenses.ExpenseId}>
                            <td></td>
                            <td>{expenses.Name}</td>
                            <td>{expenses.Price}</td>
                            <td>{expenses.Student.Name}</td>
                        </tr>
                    )}
                </tbody>
                </table>
        );
    }

}