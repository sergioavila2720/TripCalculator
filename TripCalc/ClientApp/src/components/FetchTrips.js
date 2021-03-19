import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class FetchTrips extends Component {
    constructor(props) {
        super(props);

        this.state = { trips:[], loading:true};
    }
    componentDidMount() {
        this.populateTripsData();
    }

    async populateTripsData() {
        const response = await fetch('api/tripsapi');
        const data = await response.json();

        this.setState({ trips: data, loading: false });
    }

    render() {
        let contents = this.state.loading ? <p><em>Loading...</em></p> : this.renderTripsTable(this.state.trips);

        return (
            <div>
                <h1 id="tableLabel">Trips</h1>
                <p>This fetches trips from the server</p>
                <p>
                    <Link to="/addtrip">Add Trip</Link>
                </p>
                {contents}
            </div>
            );
    }

    renderTripsTable(trips) {
        return (
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th></th>
                        <th>Trip Id</th>
                        <th>Trip Destination</th>
                        <th>Trip Total Cost</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {trips.map(trip =>
                        <tr key={trip.TripId}>
                            <td></td>
                            <td>{trip.TripId}</td>
                            <td>{trip.TripName}</td>
                            <td>{trip.TripCost}</td>
                            <td>
                                <button className="btn btn-success" onClick={(id) => this.handleEdit(trip.TripId)}  >Edit</button>&nbsp;
                                <button className="btn btn-success" onClick={(id) => this.handleViewExpenses(trip.TripId)}  >Expenses</button>&nbsp;
                                <button className="btn btn-danger" onClick={(id) => this.handleDelete(trip.TripId, trip.TripName)}  >Delete</button>&nbsp;
                            </td>
                        </tr>    
                    )}
                </tbody>
            </table>
            );
    }

    handleEdit(id) {
        this.props.history.push("/trips/edit/"+id);
    }
    handleViewExpenses(id) {
        this.props.history.push("/expensesbytrips/" + id);
    }
    handleDelete(id, name) {
        if (!window.confirm("Are you sure you want to delete trip to" + name)) {
            return;
        }
        else {
            fetch('api/tripsapi/' + id, { method: 'delete' })
                .then(data => {
                    this.setState({
                        data: this.state.trips.filter((rec) => {
                            return rec.tripId !== id;
                        })
                    });
                });
        }
    }
}

