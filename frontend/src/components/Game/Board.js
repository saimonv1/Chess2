import React, { useContext } from "react";
import Tile from "./Tile";

import classes from "./Board.module.css";
import GameContext from "../../store/game-context";

const Board = (props) => {
  const gameCtx = useContext(GameContext);

  const onFormSubmitHandler = async (event) => {
    event.preventDefault();
    
    let move = -1;
    switch (event.target.move.value) {
      case "up":
        move = 0;
        break;
      case "right":
        move = 1;
        break;
      case "left":
        move = 3;
        break;
      case "down":
        move = 2;
        break;
      default:
        move = -1;
        break;
    }
    console.log(move);
    try {
      await props.connection.invoke("SendMove", move);
    } catch (e) {
      console.log(e);
    }
  };

  const undoHandler = async event => {
    event.preventDefault();
    try {
      await props.connection.invoke("Undo");
    } catch (e) {
      console.log(e);
    }
  };

  return (
    <div className={classes.divrow}>
      <div className={classes.divcolumnsm}>
        <h1>Controls</h1>
        <form onSubmit={onFormSubmitHandler}>
          <label style={{ display: "block" }} htmlFor="move">
            Choose a move:
          </label>
          <select id="move" name="move">
            <option value="up">Up</option>
            <option value="left">Left</option>
            <option value="right">Right</option>
            <option value="down">Down</option>
          </select>
          <input type="submit" value="Submit" disabled={!gameCtx.isMyTurn}/>
        </form>
        <button onClick={undoHandler}>Undo</button>
      </div>

      <div className={classes.divcolumn}>
        <h1>Your nickname: {gameCtx.name}</h1>
        <div className={classes.grid}>
          {gameCtx.map.map((row, rowId) => {
            return (
              <div key={`Row: ${rowId}`} className={classes.row}>
                {row.map((node, nodeId) => {
                  return <Tile key={`Node: ${rowId} ${nodeId}`} row={rowId} node={nodeId} obstacle={node.isObstacle} unit={node.unit}/>;
                })}
              </div>
            );
          })}
        </div>
      </div>

      <div className={classes.divcolumnsm}>
        <h1>Players</h1>
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
            <div className={colorClass}>
              {player.turn && <b>{"»" + player.name}</b>}
              {!player.turn && <p>{player.name}</p>}
            </div>
          );
        })}
      </div>
    </div>
  );
};

export default Board;
