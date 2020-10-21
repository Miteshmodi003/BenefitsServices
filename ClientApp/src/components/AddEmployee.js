import React, { Component } from "react";

export class AddEmployee extends Component {
    // Setting the component's initial state
    state = {
        firstName: "",
        lastName: ""
    };

    handleInputChange = event => {
        // Getting the value and name of the input which triggered the change
        let value = event.target.value;
        const name = event.target.name;

        // Updating the input's state
        this.setState({
            [name]: value
        });
    };

    handleFormSubmit = event => {
        // Preventing the default behavior of the form submit (which is to refresh the page)
        event.preventDefault();
        if (!this.state.firstName || !this.state.lastName) {
            alert("Fill out your first and last name please!");

            this.setState({
                firstName: "",
                lastName: ""
            });
        };
    }

    render() {
        // Notice how each input has a `value`, `name`, and `onChange` prop
        return (
            <div>
                <p>
                    Hello {this.state.firstName} {this.state.lastName}
                </p>
                <form className="form">
                    <input
                        value={this.state.firstName}
                        name="firstName"
                        onChange={this.handleInputChange}
                        type="text"
                        placeholder="First Name"
                    />
                    <input
                        value={this.state.lastName}
                        name="lastName"
                        onChange={this.handleInputChange}
                        type="text"
                        placeholder="Last Name"
                    />

                    <button onClick={this.handleFormSubmit}>Submit</button>
                </form>
            </div>
        );
    }
}