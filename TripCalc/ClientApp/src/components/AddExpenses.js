import { extend } from 'jquery';
import React, { Component, useState } from 'react';
import Select from 'react-select'
import { useHistory } from 'react-router'

export class Expense {
    constructor() {
        this.expenseId = 0;
        this.name = "";
        this.price = 0.0;
        this.studentId = 0;
        this.tripId = 0;
    }
}
export class AddExpenses extends React.Component {

    constructor(props) {
        super(props);

        this.state = { title: "", expense: new Expense, options: [], loading: true };

        this.handleSave = this.handleSave.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }   


    async getStudentDropDown() {
        const request = await fetch('api/studentsapi/');
        const data = await request.json();

        const options = data.map(t0 => ({
            "value": t0.studentId,
            "label": t0.name
        }));
        this.setState({ title: "Create", expense: new Expense, options: options, loading: false })
        
    }

    componentDidMount() {
        this.getStudentDropDown();
    }


    render() {
        let contents = this.state.loading ? <p><em>Loading...</em></p> : this.renderCreateForm();
        
        return <div>
            <h1>{this.state.title} Expense</h1>
            <hr />
            {contents}            
        </div>
    }

    handleSave(event) {
        event.preventDefault();
        var tripId = this.props.match.params["tripId"];
        const data = new FormData(event.target);
        var response = fetch('api/expensesapi/' + tripId, { method: 'POST', body: data });
        this.props.history.push('/expensesbytrips/' + tripId);
    }

    handleCancel(event) {
        event.preventDefault();
        this.props.history.push('/fetch-trips');
    }


    renderCreateForm() {
        return (
            <form onSubmit={this.handleSave}>
                <div className="form-group row">
                    <input type="hidden" name="expenseid" value={this.state.expense.expenseId} />
                </div>
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="name" >Expense Name</label>
                    <div className="col-md-4" >
                        <input type="text" name="name" required defaultValue={this.state.expense.name} className="form-control" />
                    </div>
                    <label className="control-label col-md-12" htmlFor="price" >Expense cost</label>
                    <div className="col-md-4" >
                        <input type="number" min="0.01" step="0.01" name="price" defaultValue={this.state.expense.price} className="form-control" />
                    </div>                    
                    <label className="control-label col-md-12" htmlFor="student" >students</label>
                    <div className="col-md-4">
                        <Select name="studentId" options={this.state.options} />
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