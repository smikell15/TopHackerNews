import React, { Component } from 'react';
import { FetchData } from './FetchData';

export class Home extends Component {
  displayName = Home.name

  render() {
    return <FetchData />
  }
}
