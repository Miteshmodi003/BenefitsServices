import React, { Component } from "react";

class DependentRow extends Component {
    render() {
        const { dependent } = this.props;
        return (
            <>
                <tr>
                    <td>{dependent.n}</td>
                    <td>{dependent.firstName}</td>
                    <td>{dependent.lastName}</td>
                    <td>{dependent.relationship}</td>
                    <td>
                        <i
                            id={dependent.id}
                            onClick={(e) => {
                                console.log(e.target);
                                console.log(e.target.id);
                                this.props.handleDelete(e.target.id);
                            }}
                            className="material-icons red-text delete"
                        >
                            remove_circle
            </i>
                    </td>
                </tr>
            </>
        );
    }
}

export default DependentRow;
