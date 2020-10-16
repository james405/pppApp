import React, { Component } from 'react';
import { Accordion, Card, CardImg, Form, FormCheck } from 'react-bootstrap';
import axios from 'axios';
import region1 from './images/Regions/Region 1 Northeast.jpg';
import region2 from './images/Regions/Region 2 Midwest.jpg';
import region3 from './images/Regions/Region 3 South.jpg';
import region4 from './images/Regions/Region 4 West.jpg';
export class Regions extends Component {
    static displayName = Regions.name;


    constructor(props) {
        super(props);
        this.state = {
            regions: [{ Key: 1, EventKey: "0", DisplayName: "Northeast", Name: region1 },
                        { Key: 2, EventKey: "1", DisplayName: "Midwest", Name: region2 },
                        { Key: 3, EventKey: "2", DisplayName: "South", Name: region3 },
                        { Key: 4, EventKey: "3", DisplayName: "West", Name: region4 }],
            imgs: [],
            selectedstates: [],
            selectedImage: "",
            canDisplay: false           
        };
       
        //event handlers
        this.handleImageClick = this.handleImageClick.bind(this);
        this.handleClick = this.handleClick.bind(this);
        
    }

    componentDidMount() {
        const regions = this.state.regions;
        this.displayImages(regions);       
    }

    render() {       
        return (
            <div className="row">
                <div md={"1"}>
                    <Accordion defaultActiveKey="0" maxwidth={"300px"}>{this.state.imgs}</Accordion>
                </div>                
                <div style={{ width: '300px', height:'400px' }} md={"1"}>                    
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
            this.fetchData("Regions", this.state.selectedImage);            
        };
    }

    displayImages() {
            const regions = this.state.regions;
            const items = regions.map(region => (
                <Card style={{ width: '280px' }} key={region.Key}>
                    <Accordion.Toggle header={Card.Header} eventKey={region.EventKey}>
                        {region.DisplayName}
                    </Accordion.Toggle>
                    <Accordion.Collapse eventKey={region.EventKey}>
                        <Card>
                            <Card.Img src={region.Name} style={{ width: "275px", height: "275px" }} alt={region.DisplayName} onClick={this.handleImageClick}></Card.Img>
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

    //update the
    onUpdateItems = () => {
        this.setState(state => {
            const states = state.selectedstates.map(selectedstate => !selectedstate.checked);
            return {
                states
            };
        });
    };

    handleClick() {
        this.makeCall();       
        console.log("clicked submit");
    }

    handleImageClick(e) {
        this.setState({ selectedImage: e.target.alt }, () => {
            this.displayStates();
        });        
    }

    makeCall = () => {
        this.props.onCallBack(this.state.selectedstates);       
    }
  

   
    
}