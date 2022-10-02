import { useReducer } from "react";

import GameContext from "./game-context";

const defaultGameState = {
  players: [],
  name: "",
  color: "",
  isReady: false,
  gameStatus: false,
};

const gameReducer = (state, action) => {
  if (action.type === "ADD_PLAYER") {
    const updatedPlayers = state.players.concat(action.item);
    return { ...state, players: updatedPlayers };
  }

  if (action.type === "REMOVE_PLAYER") {
    const updatedPlayers = state.players.filter(
      (item) => item.connectionId !== action.item.connectionId
    );
    return { ...state, players: updatedPlayers };
  }

  if (action.type === "CHANGE_COLOR") {
    const updatedColor = action.item;
    return { ...state, color: updatedColor };
  }

  if (action.type === "CHANGE_NAME") {
    const updatedName = action.item;
    return { ...state, name: updatedName };
  }

  if (action.type === "CHANGE_READY_STATUS") {
    const updatedPlayers = [...state.players];
    const index = updatedPlayers.findIndex(
      (item) => item.connectionID === action.item.connectionId
    );
    updatedPlayers[index].isReady = action.item.status;
    return { ...state, players: updatedPlayers };
  }

  if (action.type === "CHANGE_MY_READY_STATUS") {
    return { ...state, isReady: action.item };
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

  const changeNameHandler = (item) => {
    dispatchGameAction({ type: "CHANGE_NAME", item: item });
  };

  const changeReadyStatusHandler = (connectionId, status) => {
    dispatchGameAction({
      type: "CHANGE_READY_STATUS",
      item: { connectionId, status },
    });
  };

  const changeMyReadyStatusHandler = (item) => {
    dispatchGameAction({ type: "CHANGE_MY_READY_STATUS", item: item });
  };

  const gameContext = {
    players: gameState.players,
    name: gameState.name,
    color: gameState.color,
    isReady: gameState.isReady,
    gameStatus: gameState.gameStatus,
    addPlayer: addPlayerHandler,
    removePlayer: removePlayerHandler,
    changeColor: changeColorHandler,
    changeName: changeNameHandler,
    changeReadyStatus: changeReadyStatusHandler,
    changeMyReadyStatus: changeMyReadyStatusHandler,
  };

  return (
    <GameContext.Provider value={gameContext}>
      {props.children}
    </GameContext.Provider>
  );
};

export default GameProvider;
