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
        this.state = { trip: new Trip, expenses: [], calculations: [], loading: true }

        this.initialize();
        this.calculateExpenses = this.calculateExpenses.bind(this);
    }

    async initialize() {
        var tripId = this.props.match.params["tripId"];
        const response = await fetch('api/tripsapi/' + tripId);
        const tripdata = await response.json();
        const expenseresponse = await fetch("api/expensesapi/tripid=" + tripId);
        const expensedata = await expenseresponse.json();


        this.setState({ trip: tripdata, expenses: expensedata, calculations: [], loading: false });
    }

    render() {
        let contents = this.state.loading ? <p><em>Loading...</em></p> : this.renderCreateForm(this.state.expenses);
        let contents2 = Object.keys(this.state.calculations).length === 0 ? <p><em></em></p> : this.renderCalculateTable(this.state.calculations);

        return (
        <div>
            <h1>Expenses made in {this.state.trip.tripName}</h1>
            <hr />                
            <div>                
                <p>This fetches expenses by trip from the server</p>
                    <p>
                        <Link to={'/addexpenses/' + this.state.trip.tripId}>Add Expense</Link>
                </p>
                </div>
                {contents}

                <button className="btn btn-info" disabled={Object.keys(this.state.expenses).length === 0 ? true : false} onClick={this.calculateExpenses}>Calculate</button>
                {contents2}                
                
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
                        <tr key={expenses.expenseId}>
                            <td></td>
                            <td>{expenses.name}</td>
                            <td>{expenses.price}</td>
                            <td>{expenses.student.name}</td>
                        </tr>
                    )}
                </tbody>
                </table>
        );
    }

    async calculateExpenses(event) {
        event.preventDefault();
        var tripId = this.props.match.params["tripId"];
        const response = await fetch('api/expensesapi/calculate/trip=' + tripId);
        const data = await response.json(); 

       

        this.setState({ trip: this.state.trip, expenses: this.state.expenses, calculations: data, loading: false });

        this.renderCalculateTable(data);


    }

    renderCalculateTable(calculations) {
        
        return (

            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th></th>
                        <th>Totals</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {calculations.map(expenses =>
                        <tr key={expenses}>
                            <td></td>
                            <td>{expenses}</td>

                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

}