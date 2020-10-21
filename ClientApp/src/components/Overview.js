import React, { Component } from 'react';

export class Overview extends Component {
    static displayName = Overview.name;

    constructor(props) {
        super(props);
        this.state = { employees: [], loading: true };
    }

    componentDidMount() {
        this.populateEmployeeData();
    }

    static renderEmployeesTable(employees) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Employee ID</th>
                        <th>Name</th>
                        <th>Dependents Count</th>
                        <th>Biweekly Deduction</th>
                        <th>Annual Benefits Cost</th>
                    </tr>
                </thead>
                <tbody>
                    {employees.map(employee =>
                        <tr key={employee.id}>
                            <td>{employee.firstName + " " + employee.lastName}</td>
                            <td>{employee.dependents.length}</td>
                            <td>{employee.benefitsCost.biweeklyDeduction}</td>
                            <td>{employee.benefitsCost.yearlyBenefitsCost}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Overview.renderEmployeesTable(this.state.employees);

        return (
            <div>
                <h1 id="tabelLabel" >All Employees Benefits Overview</h1>
                {contents}
            </div>
        );
    }

    async populateEmployeeData() {
        const response = await fetch('api/benefits');
        const data = await response.json();
        this.setState({ employees: data, loading: false });
    }
}
