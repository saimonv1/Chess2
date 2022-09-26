import React from 'react';

const GameContext = React.createContext({
    players: [],
    color: '',
    gameStatus: false,
    addPlayer: (player) => {},
    removePlayer: (id) => {},
    changeColor: (color) => {}
});

export default GameContext;