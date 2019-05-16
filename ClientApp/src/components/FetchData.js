import React, { Component } from 'react';

export class FetchData extends Component {
    displayName = FetchData.name

    constructor(props) {
        super(props);
        this.state = { stories: [], loading: true };

        fetch('api/HackerNews/bestStories')
            .then(response => response.json())
            .then(data => {
                this.setState({ stories: data, loading: false });
            });
    }

    static renderItemCard(item) {
        return (
            <div className={"col-sm-4"}>
                <div className={"card"}>
                    <div className={"card-body"}>
                        <h5 className={"card-title"}>{item.title}</h5>
                        <p className={"card-text"}>By: {item.by}</p>
                        <a className={"btn btn-primary"} href={item.url}>See Full Story</a>
                    </div>
                </div>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.state.stories.map(story => FetchData.renderItemCard(story));

        return (
            <div>
                <h1>Today's Top Stories</h1>
                {contents}
            </div>
        );
    }
}
