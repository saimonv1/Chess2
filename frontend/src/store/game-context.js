import React from 'react';

const GameContext = React.createContext({
    players: [],
    gameStatus: false,
    map: [],
    movesLeft: 0,
    pickupsLeft: 0,

    name: '',
    color: -1,
    currentUnit: 0,
    isReady: false,
    isMyTurn: false,
    canRevert: false,

    isInvalidCommand: false,

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
    setPickupsLeft: (pickups) => {},
    changeCurrentUnit: (unit) => {},
    changeInvalidCommand: (isInvalidCommand) => {},
    setCanRevert: (canRevert) => {},
});

export default GameContext;