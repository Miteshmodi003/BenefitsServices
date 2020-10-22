import React, { Component } from "react";
import DependentRow from "./DependentRow";
import * as V from "uuid";

export default class DependentTable extends Component {
  render() {
    const { dependents } = this.props;

    var n = 1;

    return (
      <div>
        <h5>Dependents</h5>
        <table>
          <thead>
            <tr>
              <th>I.D</th>
              <th>FirstName</th>
              <th>Last Name</th>
              <th>RelationShip</th>
            </tr>
          </thead>
          <tbody>
            {dependents.map((dependent) => {
              dependent = { ...dependent, n };
              n++;

              return (
                <DependentRow
                  key={V.v1()}
                  dependent={dependent}
                  handleDelete={this.props.handleDelete}
                />
              );
            })}
          </tbody>
        </table>
      </div>
    );
  }
}
