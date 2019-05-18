import React, { Component } from 'react';

export class ItemGrid extends Component {
    displayName = ItemGrid.name

    constructor(props) {
        super(props);
        this.state = { initialStories: [], stories: [], loading: true };

        this.doSearch = this.doSearch.bind(this);

        fetch('api/HackerNews/bestStories')
            .then(response => response.json())
            .then(data => {
                this.setState({ initialStories: data, stories: data, loading: false });
            });
    }

    static renderItemCard(item) {
        return (
            <div className={"col-sm-4"} key={item.id}>
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

    doSearch(e) {
        var searchVal = e.target.value || '';
        this.setState({
            stories: this.state.initialStories.filter(function (item) {
                return item.title.includes(searchVal) || item.by.includes(searchVal);
            })
        });
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.state.stories.map(story => ItemGrid.renderItemCard(story));

        return (
            <div>
                <h1>Today's Top Stories</h1>
                <div className={"form-group"}>
                    <div className={"col-sm-12"}>
                        <input type={"text"} className={"form-control"} id={"searchBar"} placeholder={"Search"} onChange={this.doSearch} />
                    </div>
                </div>
                {contents}
            </div >
        );
    }
}
