import React, { useContext } from "react";
import GameContext from "../../store/game-context";

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
      <ul>
        {gameCtx.players.map((player) => {
          return (
            <li key={player.connectionId}>
              <h3>{player.name}</h3>
              <p>{player.color}</p>
              <p>{player.isReady ? "Ready" : "Not ready"}</p>
            </li>
          );
        })}
      </ul>
      <button onClick={onClickHandler}>
        {gameCtx.isReady ? "UnReady" : "Ready"}
      </button>
    </React.Fragment>
  );
};

export default Lobby;
