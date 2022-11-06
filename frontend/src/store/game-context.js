import React from 'react';

const GameContext = React.createContext({
    players: [],
    gameStatus: false,
    map: [],
    movesLeft: 0,

    name: '',
    color: -1,
    currentUnit: 0,
    isReady: false,
    isMyTurn: false,

    addPlayer: (player) => {},
    removePlayer: (id) => {},

    changeColor: (color) => {},
    changeName: (name) => {},
    
    changeReadyStatus: (connectionId, status) => {},
    changeMyReadyStatus: (status) => {},

    changeTurnStatus: (oldTurnId, newTurnId) => {},

    changeGameStatus: (status) => {},
    insertMap: (map) => {},
    gameMove: (fromX, fromY, toX, toY) => {},

    setMovesLeft: (moves) => {},
    changeCurrentUnit: (unit) => {},
});

export default GameContext;