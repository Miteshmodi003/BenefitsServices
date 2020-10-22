import React, { Component } from "react";
import AddDependent from "./AddDependent";
import { AutoInit } from "materialize-css";
import DisplayError from "./DisplayError";
import DependentTable from "./DependentTable";
import * as V from "uuid";
import { Redirect } from "react-router-dom";

export class AddEmployee extends Component {
    state = {
        employee_id: V.v1(),
        firstname: null,
        lastname: null,
        errors: { firstName: null, lastName: null },
        employee: [],
        dependents: [],
        displayError: false,
        redirect: null,
    };
    componentDidMount() {
        AutoInit();
    }

    handleAddDependent = (dependent) => {
        dependent = {
            ...dependent,
            id: V.v1(),
        };
        this.setState({ dependents: [...this.state.dependents, dependent] });
    };
    handleChange = (e) => {
        e.preventDefault();
        const { name, value } = e.target;
        let errors = this.state.errors;
        switch (name) {
            case "firstname":
                errors.firstName = /^[A-Za-z]*$/.test(value)
                    ? null
                    : "No special character,number or space is allowed in name";
                break;

            case "lastname":
                errors.lastName = /^[A-Za-z]*$/.test(value)
                    ? null
                    : "No special character,number or space is allowed in lastname";
                break;
        }
        this.setState({ errors, [name]: this.turnFirstToCapital(value) }, () => { });
    };
    handleDelete = (id) => {
        let prevDependents = [...this.state.dependents];
        const newDependents = prevDependents.filter(
            (dependent) => dependent.id != id
        );
        this.setState({ dependents: newDependents });
    };
    handleSubmit = async (e) => {
        e.preventDefault();
        e.target.firstname.value = "";
        e.target.lastname.value = "";
        if (
            this.state.errors.firstName ||
            this.state.errors.lastName ||
            !this.state.firstname ||
            !this.state.lastname
        ) {
            this.setState({ displayError: true });
            setTimeout(() => this.setState({ displayError: false }), 2000);
        } else {
            const newDependents = this.state.dependents.map((dependent) => {
                delete dependent.id;
                return dependent;
            });

            let person = {
                firstName: this.state.firstname,
                lastName: this.state.lastname,
                dependents: newDependents,
            };

            const returnedValue = await fetch("/api/benefits", {
                method: "POST",
                headers: { "Content-Type": "application/json" },

                body: JSON.stringify(person), //If the route is incorrect please correct it
            });
            this.setState({ redirect: "/overview" });

            // this.setState({ dependents: [] });
            // this.setState({ firstName: null });
            // this.setState({ lastName: null });
            // this.setState({ employee: {} });
            // alert("The employee is added, check in the overview page");
        }
    };
    turnFirstToCapital = (value) => {
        if (value) {
            var splitted = value.split("");
            splitted[0] = splitted[0].toUpperCase();
            return splitted.join("");
        }
        return value;
    };
    render() {
        if (this.state.redirect) {
            return <Redirect to={this.state.redirect} />;
        }
        return (
            <div>
                {this.state.displayError && <DisplayError />}

                <div className="container section">
                    <form className="white section" onSubmit={this.handleSubmit}>
                        <h5 className="grey-text text-darken">Create an employee</h5>
                        <div className="input-field">
                            <label htmlFor="firstname">First Name</label>
                            <input
                                type="text"
                                id="firstname"
                                name="firstname"
                                autoCapitalize="words"
                                className={this.state.errors.firstName ? "show" : "unshow"}
                                onChange={this.handleChange}
                            />
                            <p>{this.state.errors.firstName}</p>
                        </div>
                        <div className="input-field">
                            <label htmlFor="lastname">Last Name</label>
                            <input
                                type="text"
                                id="lastname"
                                name="lastname"
                                autoCapitalize="words"
                                className={this.state.errors.lastName ? "show" : "unshow"}
                                onChange={this.handleChange}
                            />
                            <p>{this.state.errors.lastName}</p>
                        </div>
                        {this.state.dependents.length ? (
                            <DependentTable
                                dependents={this.state.dependents}
                                handleDelete={this.handleDelete}
                            />
                        ) : null}
                        <a className="modal-trigger" href="#dependentModal">
                            <button type="button" className="btn">
                                {this.state.dependents.length
                                    ? "Add more dependents"
                                    : "Add Dependent"}
                            </button>
                        </a>
                        <div className="input-field">
                            <button className="btn lighten-1">Create</button>
                        </div>
                    </form>
                    <AddDependent handleAddDependent={this.handleAddDependent} />
                </div>
            </div>
        );
    }
}
