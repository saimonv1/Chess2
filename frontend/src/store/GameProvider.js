import { useReducer } from 'react';

import GameContext from './game-context';

const defaultGameState = {
    players: [],
    color: 'yellow',
    gameStatus: false
};

const gameReducer = (state, action) => {
    if(action.type === "ADD_PLAYER"){

    }

    if(action.type === "REMOVE_PLAYER"){

    }

    if(action.type === "CHANGE_COLOR"){

    }

    return defaultGameState;
};

const GameProvider = (props) => {
    const [gameState, dispatchGameAction] = useReducer(
        gameReducer,
        defaultGameState
    );

    const addPlayerHandler = (item) => {
        dispatchGameAction({ type: "ADD_PLAYER", item: item });
    };

    const removePlayerHandler = (item) => {
        dispatchGameAction({ type: "REMOVE_PLAYER", item: item });
    };

    const changeColorHandler = (item) => {
        dispatchGameAction({ type: "CHANGE_COLOR", item: item });
    };

    const gameContext = {
        players: gameState.players,
        color: gameState.color,
        addPlayer: addPlayerHandler,
        removePlayer: removePlayerHandler,
        changeColor: changeColorHandler
    };

    return (
        <GameContext.Provider value={gameContext}>
            {props.children}
        </GameContext.Provider>
    );
};

export default GameProvider;