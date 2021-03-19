import { extend } from 'jquery';
import React, { Component } from 'react';

export class Trip {
    constructor() {
        this.TripId = 0;
        this.TripName = "";
        this.TripCost = 0.0;
    }
}

export class AddTrip extends Component {
    constructor(props) {
        super(props);

        this.state = { title: "", trip: new Trip, loading: true };

        this.initialize();
        this.handleSave = this.handleSave.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }

    async initialize() {
        var tripId = this.props.match.params["tripId"];
        if (tripId > 0) {
            const response = await fetch('api/tripsapi/' + tripId);
            const data = await response.json();

            this.setState({
                title: "Edit", trip: data, loading: false
            });
        }
        else {
            this.state = {
                title: "Create", trip: new Trip, loading: false
            };
        }
    }

    render() {
        let contents = this.state.loading ? <p><em>Loading...</em></p> : this.renderCreateForm();

        return <div>
            <h1>{this.state.title}</h1>
            <h3>Trip</h3>
            <hr />
            {contents}
        </div>
    }

    handleSave(event) {
        event.preventDefault();

        const data = new FormData(event.target);

        if (this.state.trip.TripId) {
            var response1 = fetch('api/tripsapi/' + this.state.trip.TripId, { method: 'PUT', body: data });
            this.props.history.push('/fetch-trips');
        }
        else {
            var response2 = fetch('api/tripsapi', { method: 'POST', body: data });
            this.props.history.push('/fetch-trips');
        }
    }

    handleCancel(event) {
        event.preventDefault();
        this.props.history.push('/fetch-trips');
    }

    renderCreateForm() {
        return (
            <form onSubmit={this.handleSave}>
                <div className="form-group row">
                    <input type="hidden" name="tripId" value={this.state.trip.TripId} />
                </div>
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="tripName" >Name</label>
                    <div className="col-md-4" >
                        <input type="text" name="tripName" defaultValue={this.state.trip.TripName} className="form-control" />
                    </div>
                    <label className="control-label col-md-12" htmlFor="TripCost" >Cost</label>
                    <div className="col-md-4" >
                        <input type="number" min="0.01" step="0.01" name="TripCost" defaultValue={this.state.trip.TripCost} className="form-control" />
                    </div>
                </div>

                <div className="form-group row">
                    <button type="submit" className="btn btn-success">Save</button>
                    <button className="btn btn-danger" onClick={this.handleCancel}>Cancel</button>
                </div>
            </form>
        );
    }
}
