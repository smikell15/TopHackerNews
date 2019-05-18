import React from "react"
import Enzyme, { shallow, mount } from "enzyme"
import { ItemGrid } from "./ItemGrid"
import Adapter from "enzyme-adapter-react-16"

Enzyme.configure({ adapter: new Adapter() });

beforeEach(function () {

    global.fetch = jest.fn().mockImplementation(() => {
        var p = new Promise((resolve, reject) => {
            resolve({
                ok: true,
                id: '123',
                json: function () {
                    return [{ }]
                }
            });
        });

        return p;
    });

});

describe("ItemGrid component", () => {
    test("renders without crashing", () => {

        const wrapper = shallow(<ItemGrid />);

        expect(wrapper.exists()).toBe(true);
    });

    test("should filter stories by filter text", () => {

        const wrapper = shallow(<ItemGrid />);
        wrapper.setState({
            initialStories: [
                { id: '1', by: 'author1', title: 'title1', url: 'url1' },
                { id: '2', by: 'author2', title: 'title2', url: 'url2' }
            ],
            stories: [
                { id: '1', by: 'author1', title: 'title1', url: 'url1' },
                { id: '2', by: 'author2', title: 'title2', url: 'url2' }
            ],
            loading: false
        });
        const event = { target: { value: 'or2' } };

        wrapper.instance().doSearch(event);

        expect(wrapper.state('stories')).toEqual([{ id: '2', by: 'author2', title: 'title2', url: 'url2' }]);
    });

    test("should initially show loading caption", () => {

        const wrapper = shallow(<ItemGrid />);
        wrapper.setState({ loading: true });

        expect(wrapper.containsMatchingElement(<p><em>Loading...</em></p>)).toBeTruthy();
    });

    test("should not show loading caption after load", () => {

        const wrapper = shallow(<ItemGrid />);
        wrapper.setState({ loading: false });

        expect(wrapper.containsMatchingElement(<p><em>Loading...</em></p>)).toBeFalsy();
    });
});