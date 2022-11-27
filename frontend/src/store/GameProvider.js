import { useReducer } from "react";

import GameContext from "./game-context";

const defaultGameState = {
  players: [],
  gameStatus: false,
  map: [],
  movesLeft: 0,
  pickupsLeft: 0,

  name: "",
  color: -1,
  currentUnit: 0,
  isReady: false,
  isMyTurn: false,
};

const gameReducer = (state, action) => {
  if (action.type === "ADD_PLAYER") {
    const updatedPlayers = state.players.concat(action.item);
    return { ...state, players: updatedPlayers };
  }

  if (action.type === "REMOVE_PLAYER") {
    const updatedPlayers = state.players.filter(
      (player) => player.connectionID !== action.item.connectionID
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

  if (action.type === "CHANGE_GAME_STATUS") {
    return { ...state, gameStatus: action.item };
  }

  if(action.type === "CHANGE_TURN_STATUS") {
    let newIsMyTurn = state.isMyTurn;
    const newPlayers = [...state.players];
    if(action.item.oldTurnId) {
      const playerIndex = newPlayers.findIndex(p => p.connectionID === action.item.oldTurnId);
      let player = newPlayers[playerIndex];
      player = {...player, turn: false};
      if(player.name === state.name) {
        newIsMyTurn = false;
      }
      newPlayers[playerIndex] = player;
    }

    const playerIndex = newPlayers.findIndex(p => p.connectionID === action.item.newTurnId);
    let player = newPlayers[playerIndex];
    player = {...player, turn: true};
    if(player.name === state.name) {
      newIsMyTurn = true;
    }
    newPlayers[playerIndex] = player;

    return { ...state, players: newPlayers, isMyTurn: newIsMyTurn };
  }

  if (action.type === "INSERT_MAP") {
    return { ...state, map: action.item };
  }

  if (action.type === "GAME_MOVE") {
    let newMap = [...state.map];
    const temp = newMap[action.item.toX][action.item.toY];
    newMap[action.item.toX][action.item.toY] = newMap[action.item.fromX][action.item.fromY];
    newMap[action.item.fromX][action.item.fromY] = temp;
    return { ...state, map: newMap };
  }

  if (action.type === "MOVES_LEFT") {
    return { ...state, movesLeft: action.item };
  }

  if (action.type === "PICKUPS_LEFT") {
    return { ...state, pickupsLeft: action.item };
  }

  if (action.type === "CURRENT_UNIT") {
    return { ...state, currentUnit: action.item };
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

  const changeGameStatusHandler = (status) => {
    dispatchGameAction({ type: "CHANGE_GAME_STATUS", item: status });
  };

  const changeTurnStatusHandler = (oldTurnId, newTurnId) => {
    dispatchGameAction({ type: "CHANGE_TURN_STATUS", item: { oldTurnId, newTurnId } });
  }

  const insertMapHandler = (map) => {
    dispatchGameAction({ type: "INSERT_MAP", item: map });
  };

  const gameMoveHandler = (fromX, fromY, toX, toY) => {
    dispatchGameAction({
      type: "GAME_MOVE",
      item: { fromX: fromX, fromY: fromY, toX: toX, toY: toY },
    });
  };

  const setMovesLeftHandler = (moves) => {
    dispatchGameAction({
      type: "MOVES_LEFT",
      item: moves,
    });
  };

  const setPickupsLeftHandler = (pickups) => {
    dispatchGameAction({
      type: "PICKUPS_LEFT",
      item: pickups,
    });
  };

  const changeCurrentUnitHandler = (unit) => {
    dispatchGameAction({
      type: "CURRENT_UNIT",
      item: unit,
    });
  };

  const gameContext = {
    players: gameState.players,
    gameStatus: gameState.gameStatus,
    map: gameState.map,
    movesLeft: gameState.movesLeft,
    pickupsLeft: gameState.pickupsLeft,

    name: gameState.name,
    color: gameState.color,
    currentUnit: gameState.currentUnit,
    isReady: gameState.isReady,
    isMyTurn: gameState.isMyTurn,

    addPlayer: addPlayerHandler,
    removePlayer: removePlayerHandler,
    changeColor: changeColorHandler,
    changeName: changeNameHandler,

    changeReadyStatus: changeReadyStatusHandler,
    changeMyReadyStatus: changeMyReadyStatusHandler,
    changeTurnStatus: changeTurnStatusHandler,

    changeGameStatus: changeGameStatusHandler,
    insertMap: insertMapHandler,
    gameMove: gameMoveHandler,
    setMovesLeft: setMovesLeftHandler,
    setPickupsLeft: setPickupsLeftHandler,
    changeCurrentUnit: changeCurrentUnitHandler,
  };

  return (
    <GameContext.Provider value={gameContext}>
      {props.children}
    </GameContext.Provider>
  );
};

export default GameProvider;
