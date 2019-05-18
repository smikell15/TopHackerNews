import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';

beforeEach(function () {

    global.fetch = jest.fn().mockImplementation(() => {
        var p = new Promise((resolve, reject) => {
            resolve({
                ok: true,
                id: '123',
                json: function () {
                    return [{ id: '123'}]
                }
            });
        });

        return p;
    });

});

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(<App />, div);
});
