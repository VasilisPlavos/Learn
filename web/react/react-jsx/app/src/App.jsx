import React, { Component } from "react";
import NavBar from "./NavBar";
import MainContent from "./MainContent";

class App extends Component {
    render() {
        return (
            <React.Fragment>
                <NavBar/>
                <MainContent/>
            </React.Fragment>
        );
    }
}

export default App;