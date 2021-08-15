import React, { Component } from "react";
class MainContent extends Component {

    state = {
        appTitle: "Employees",
        customersCount: 5,
        customers: [
            {id: 1, name: "Scott", phone: "123-456"},
            {id: 2, name: "Jane", phone: "123-457"},
            {id: 3, name: "Ann", phone: "123-458"}
        ]
    };

    render() {
        return (
            <div>
                <h4 className="border-bottom m-1 p-1">
                    {this.state.appTitle}
                    <span className="badge bg-secondary m-2">{this.state.customersCount}</span>
                    <span className={"btn btn-info"} onClick={this.onRefreshClick}>Refresh</span>
                </h4>
                <table className={"table"}>
                    <thead>
                    <tr>
                        <th>#</th>
                        <th>Customer Name</th>
                        <th>Phone</th>
                    </tr>
                    </thead>
                    <tbody>
                    { this.state.customers.map((c) => {
                        return(
                            <tr key={c.id}>
                                <td>{c.id}</td>
                                <td>{c.name}</td>
                                <td>{c.phone}</td>
                            </tr>
                        );
                    }) }
                    </tbody>
                </table>
            </div>
        );
    }

    onRefreshClick = () => {
        let counter = this.state.customersCount + 1;
        this.setState({customersCount: counter});
    }
}

export default MainContent;