import React, { Component } from 'react';
import { Form, FormButton, FormGroup, FormLabel } from 'react-bootstrap';
import Filters from './Filters';

export class PPPData extends Component {
    static displayName = PPPData.name;

    constructor(props) {
        super(props);
        this.state = {
            toggleFilters: true,
            stateList: []            
        };

        this.toggle = this.toggle.bind(this);
    }

   

    toggle() {
        this.setState({ toggleFilters: !this.state.toggleFilters });
    }

    callBack = (selectedstates) => {
        this.setState({ stateList: selectedstates }, () => { console.log("Return selected states") });
    }


    render() {
        const { toggleFilters } = this.state;
        return (
            <div className='row'>
                <div>Search Filters
                { toggleFilters && <Filters /> }
                </div>
                <div style={{ marginLeft: '90%'}}>
                    <button className="btn, btn-default" onClick={this.toggle}>Show/Hide</button>
                </div>
            </div>
        );
    }
}
