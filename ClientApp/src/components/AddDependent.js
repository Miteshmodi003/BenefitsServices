import React, { Component } from "react";
import { Modal } from "reactstrap";
import * as M from "materialize-css";
import DisplayError from "./DisplayError";

M.AutoInit();
export default class AddDependent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      values: [],
      firstname: null,
      lastname: null,
      errors: { firstName: null, lastName: null },
      displayError: false,

      relation: "Spouse",
    };
  }
  turnFirstToCapital = (value) => {
    if (value) {
      var splitted = value.split("");
      splitted[0] = splitted[0].toUpperCase();
      return splitted.join("");
    }
    return value;
  };

  handleSubmit = (e) => {
    const { handleAddDependent } = this.props;

    e = e || window.event;
    e.preventDefault();
    if (
      this.state.errors.firstName ||
      this.state.errors.lastName ||
      !this.state.firstname ||
      !this.state.lastname
    ) {
      this.setState({ displayError: true });
      setTimeout(() => this.setState({ displayError: false }), 2000);
    } else {
      handleAddDependent({
        firstName: this.state.firstname,
        lastName: this.state.lastname,
        relationship: this.state.relation,
      });
      e.target.firstname.value = "";
      e.target.lastname.value = "";
      this.state.firstname = null;
      this.state.lastname = null;
      M.Modal.getInstance(document.getElementById("dependentModal")).close();
    }
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
      case "relation":
        break;
    }
    this.setState({ errors, [name]: this.turnFirstToCapital(value) }, () => {});
  };

  render() {
    return (
      <div
        style={{ background: "none" }}
        className="modal "
        id="dependentModal"
      >
        <div className="modal-content container" style={{ marginTop: "40px" }}>
          {this.state.displayError && <DisplayError />}
          <h5 className="grey-text text-darken">Add dependent</h5>
          <form onSubmit={this.handleSubmit} className="container section">
            <div className="input-field">
              <label htmlFor="firstname">FirstName</label>
              <input
                type="text"
                id="firstname"
                name="firstname"
                minLength="2"
                className={this.state.errors.firstName ? "show" : "unshow"}
                onChange={(e) => {
                  this.handleChange(e);
                }}
              />
              <p>{this.state.errors.firstName}</p>
            </div>
            <div className="input-field">
              <label htmlFor="lastname">LastName</label>
              <input
                type="text"
                id="lastname"
                name="lastname"
                minLength="2"
                className={this.state.errors.lastName ? "show" : "unshow"}
                onChange={(e) => {
                  e = e || window.event;

                  this.handleChange(e);
                }}
              />
              <p>{this.state.errors.lastName}</p>
            </div>

            <select
              id="realtion"
              name="relation"
              style={{ display: "block" }}
              onChange={(e) => {
                e = e || window.event;
                this.handleChange(e);
              }}
            >
              <option value="spouse">Spouse</option>
              <option value="child">Child</option>
            </select>
            <button
              style={{ marginTop: "10px" }}
              type="submit"
              className="btn green lighten-2"
            >
              Add
            </button>
          </form>
        </div>
      </div>
    );
  }
}
