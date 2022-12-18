import { screen, render } from '@testing-library/react';
import React from 'react';
import App from './App';

describe('App component', () => {
    it('setupConnection works', async () => {
        render(<App />);

        setTimeout(() =>
        {
            expect(screen.getByDisplayValue('Submit')).toBeInTheDocument();
        }, 1500);
    });
});