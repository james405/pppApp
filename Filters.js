import React, { Component } from 'react';
import { Tabs, Tab } from 'react-bootstrap';
import { Regions } from './Regions';
import { Divisions } from './Divisions';
import { Otherfilters } from './Otherfilters';
export default class Filters extends Component {
    static displayName = Filters.name;

    constructor(props) {
        super(props);
        this.state = {
            stateList: [],            
            lenderName: "",
            city: "",
            businessName: "",
            jobsRetained: 0,
        };
    }

    callBack = (selectedstates) => {
        this.setState({ stateList: selectedstates }, () => { console.log("Selected states") });
    }

    otherCallBack = (otherfilters) => {
        this.setState({ businessName: otherfilters.businessName.value, lenderName: otherfilters.lenderName.value, city: otherfilters.city.value, jobsRetained: otherfilters.jobsRetained.value },
                () => { console.log(otherfilters.businessName.value + " " + otherfilters.lenderName.value + " " + otherfilters.city.value + " " + otherfilters.jobsRetained.value) });
    }

    render() {
        return (
            <div>
                <Tabs defaultActiveKey="regions" id="filtersTab">
                    <Tab eventKey="regions" title="Regions">
                        <Regions onCallBack={this.callBack} />
                    </Tab>
                    <Tab eventKey="divisions" title="Divisions">
                        <Divisions onCallBack={this.callBack} />
                    </Tab>
                    <Tab eventKey="other" title="Other Filters">
                        <Otherfilters onCallBack={this.otherCallBack} />
                    </Tab>
                </Tabs>
            </div>
        );
    }
}
