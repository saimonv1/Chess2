import React, { useContext } from "react";
import GameContext from "../../store/game-context";

import classes from "./Lobby.module.css";

const Lobby = (props) => {
  const gameCtx = useContext(GameContext);

  const onClickHandler = async (event) => {
    try {
      await props.connection.invoke("ReadyUp");
      gameCtx.changeMyReadyStatus(!gameCtx.isReady);
    } catch (e) {
      console.log(e);
      //props.setError(`${e}`);
    }
  };

  return (
    <React.Fragment>
      <h1>Lobby</h1>
      {gameCtx.lastWinner && <div><b>Last winner:</b>{gameCtx.lastWinner}</div>}
      {gameCtx.players.map((player) => {
          let colorClass = classes.red;
          if (player.color === 0) {
            colorClass = classes.red;
          } else if (player.color === 1) {
            colorClass = classes.blue;
          } else if (player.color === 2) {
            colorClass = classes.green;
          } else if (player.color === 3) {
            colorClass = classes.yellow;
          }
          return (
            <div key={player.connectionID} className={colorClass}>
              {player.isReady ? '✔' : 'X'} <b>{player.name}</b>
            </div>
          );
      })}
      <button onClick={onClickHandler}>
        {gameCtx.isReady ? "UnReady" : "Ready"}
      </button>
    </React.Fragment>
  );
};

export default Lobby;
