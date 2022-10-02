import React from 'react';

const GameContext = React.createContext({
    players: [],
    name: '',
    color: '',
    isReady: false,
    gameStatus: false,
    addPlayer: (player) => {},
    removePlayer: (id) => {},
    changeColor: (color) => {},
    changeName: (name) => {},
    changeReadyStatus: (connectionId, status) => {},
    changeMyReadyStatus: (status) => {},
});

export default GameContext;