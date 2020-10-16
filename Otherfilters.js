import React, { Component } from 'react';
import {Form, Row, Col} from 'react-bootstrap';

export class Otherfilters extends Component {
    static displayName = Otherfilters.name;

    constructor(props) {
        super(props);
        this.state = {
            businessName: "",
            lenderName: "",
            city: "",
            jobsRetained: 0
        };

        this.handleClick = this.handleClick.bind(this);
        this.handleBusinessChange = this.handleBusinessChange.bind(this);
        this.handleLenderChange = this.handleLenderChange.bind(this);
        this.handleCityChange = this.handleCityChange.bind(this);
        this.handleJobsChange = this.handleJobsChange.bind(this);
    }

    render() {
        return (
            <div md={"5"}>
                <Form>
                    <Form.Group as={Row} controlId="businessName">
                        <Form.Label column sm={6}>
                            Business Name
                        </Form.Label>
                        <Col sm={10}>
                            <Form.Control type="text" onChange={this.handleBusinessChange} placeholder="Business Name" />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="lenderName">
                        <Form.Label column sm={6}>
                            Lender Name
                        </Form.Label>
                        <Col sm={10}>
                            <Form.Control type="text" onChange={this.handleLenderChange}  placeholder="Lender Name" />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="city">
                        <Form.Label column sm={6}>
                            City
                        </Form.Label>
                        <Col sm={10}>
                            <Form.Control type="text" onChange={this.handleCityChange} placeholder="City" />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="jobsRetained">
                        <Form.Label column sm={6}>
                            Jobs Retained
                        </Form.Label>
                        <Col sm={10}>
                            <Form.Control type="number" onChange={this.handleJobsChange}  placeholder="Jobs Retained" />
                        </Col>
                    </Form.Group>
                    <Form.Row className="align-item-left">
                        <button type="button" className="btn, btn-default" onClick={this.handleClick} >Submit</button>
                    </Form.Row>
                </Form>
            </div>

        );
    };

     //this.setState({ selectedImage: e.target.alt }, () => {
     //    this.displayStates();
     //});        
    handleBusinessChange(e) {
        this.setState({ businessName: e.target }, () => {
                console.log("Business name changed");
            }
        );
    }

    handleLenderChange(e) {
        this.setState({ lenderName: e.target }, () => {
                console.log("Lender name changed");
            }
        );
    }

    handleCityChange(e) {
        this.setState({ city: e.target }, () => {
                console.log("City name changed");
            }
        );
    }

    handleJobsChange(e) {
        this.setState({ jobsRetained: e.target }, () => {
                console.log("Jobs retained changed");
            }
        );
    }

    handleClick() {        
        this.makeCall();
        console.log("clicked submit");
    }

    makeCall = () => {
        this.props.onCallBack(this.state);
    }

}