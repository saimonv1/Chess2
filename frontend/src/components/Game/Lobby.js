import React, { useContext, useState } from "react";
import GameContext from "../../store/game-context";

const Lobby = (props) => {
    const [isReady, setIsReady] = useState(false);

  const gameCtx = useContext(GameContext);

    const onClickHandler = (event) => {
        setIsReady(prevState => !prevState);
    }

  return (
    <React.Fragment>
      <h1>Lobby</h1>
      {gameCtx.players.map((player) => {
        return <div><h3>player.name</h3></div>;
      })}
      <button onClick={onClickHandler}>{isReady ? 'UnReady' : 'Ready'}</button>
    </React.Fragment>
  );
};

export default Lobby;
