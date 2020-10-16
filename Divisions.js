import React, { Component } from 'react';
import axios from 'axios';
import { Accordion, Card, CardImg, Form, FormCheck } from 'react-bootstrap';
import EastNorthCentral from './images/Divisions/EastNorthCentralDivision.jpg';
import EastSouthCentral from './images/Divisions/EastSouthCentralDivision.jpg';
import MidAtlantic from './images/Divisions/MidAtlanticDivision.jpg';
import Mountain from './images/Divisions/MountainDivision.jpg';
import NewEngland from './images/Divisions/NewEnglandDivision.jpg';
import Pacific from './images/Divisions/PacificDivision.jpg';
import SouthAtlantic from './images/Divisions/SouthAtlanticDivision.jpg';
import WestNorthCentral from './images/Divisions/WestNorthCentralDivision.jpg';
import WestSouthCentral from './images/Divisions/WestSouthCentralDivision.jpg';

export class Divisions extends Component {
    static displayName = Divisions.name;


    constructor(props) {
        super(props);
        this.state = {
            divisions: [{ Key: 1, EventKey:"0",DisplayName: 'East North Central',Name: EastNorthCentral },
                { Key: 2, EventKey: "1", DisplayName: 'East South Central',Name: EastSouthCentral },
                { Key: 3, EventKey: "2",DisplayName: 'Mid Atlantic', Name: MidAtlantic },
                { Key: 4, EventKey: "3",DisplayName: 'Mountain',Name: Mountain },
                { Key: 5, EventKey: "4", DisplayName: 'New England',Name: NewEngland },
                { Key: 6, EventKey: "5",DisplayName: 'Pacific',Name: Pacific },
                { Key: 7, EventKey: "6",DisplayName: 'South Atlantic',Name: SouthAtlantic },
                { Key: 8, EventKey: "7",DisplayName: 'West North Central',Name: WestNorthCentral },
                { Key: 9, EventKey: "8",DisplayName: 'West South Central',Name: WestSouthCentral }],
            imgs: [],
            selectedstates:[],
            selectedImage: "",
            canDisplay:false
        };

        //event handlers
        this.handleImageClick = this.handleImageClick.bind(this);
        this.handleClick = this.handleClick.bind(this);
    }

    componentDidMount() {
        const divisions = this.state.divisions;
        this.displayImages(divisions);
        
    }

    render() {
        return (
           <div className="row">
                <div md={"1"}>
                    <Accordion defaultActiveKey="0"  maxwidth={"300px"}>{this.state.imgs}</Accordion>
                </div>
                <div style={{ width: '300px', height: '400px' }} md={"1"}>
                    <table className='table' aria-labelledby="tableLabel">
                        <thead>
                            <tr>
                                <th>State Name</th>
                                <th>Abbr</th>
                                <th>Include</th>
                            </tr>
                        </thead>
                        <tbody>{this.state.selectedstates}</tbody>
                    </table>
                </div>
                <div md={"1"}>
                    <button onClick={this.handleClick} >Select</button>
                </div>
            </div>
        );
    }

    displayStates() {
        if (this.state.selectedImage !== undefined) {
            this.canDisplay = true;
            this.fetchData("Divisions", this.state.selectedImage);
            return;
        };
    }

    displayImages() {
        const divisions = this.state.divisions;
        const items = divisions.map(division => (
            <Card style={{ width:'280px'}} key={division.Key}>              
                <Accordion.Toggle header={Card.Header} eventKey={division.EventKey}>
                    {division.DisplayName} 
                </Accordion.Toggle>
                <Accordion.Collapse eventKey={division.EventKey}>
                    <Card>                        
                        <Card.Img src={division.Name} style={{ width: "275px", height: "275px" }} alt={division.DisplayName} onClick={this.handleImageClick}></Card.Img>
                    </Card>
                </Accordion.Collapse>                
            </Card>            
        ));

        return (
            this.setState({ imgs: items })
        );
    };

    async fetchData(_type, _name) {
        let url = "https://localhost:44304/home/getStates?type=" + _type + "&name=" + _name;
        await axios.get(url,
            { crossDomain: true })
            .then(res => {
                const mystates = res.data;
                const items = mystates.map(mystate => {
                    return (
                        <tr key={mystate.StateId}>
                            <td>{mystate.Name}</td>
                            <td>{mystate.Abbreviation}</td>
                            <td><Form.Check type="checkbox" defaultChecked={true} onChange={this.onUpdateItems}></Form.Check></td>
                        </tr>
                    )
                });

                this.setState({ selectedstates: items }, () => { return })
            });
    };   

    onUpdateItems = () => {
        this.setState(state => {
            const states = state.selectedstates.map(selectedstate => !selectedstate.checked);
            return {
                states
            };
        });
    };

    handleImageClick(e) {
        this.setState({ selectedImage: e.target.alt + 'Division' }, () => {
            this.displayStates();
        });
    }

    handleClick() {
        this.makeCall();
        console.log("clicked submit");
    }

    makeCall = () => {
        this.props.onCallBack(this.state.selectedstates);
    }

}