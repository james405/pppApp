import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { PPPData } from './components/PPPData';
import { GitHub } from './components/GitHub';
import './bootstrapcss/bootstrap.css';
import './custom.css'
export default class App extends Component {
  static displayName = "Payroll Protection Plan Data"

  render () {
      return (
       <div>        
        <Layout>            
                <Route path='/ppp-data' component={PPPData} />                    
                <Route path='/git-hub' component={GitHub} />                
         </Layout>         
        </div>
    );
  }
}


