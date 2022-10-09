import React, { useContext } from "react";
import Tile from "./Tile";

import classes from "./Board.module.css";
import GameContext from "../../store/game-context";

// const GRID_ROW_LENGTH = 20;
// const GRID_COL_LENGTH = 20;

const Board = (props) => {
  const gameCtx = useContext(GameContext);

//   const grid = [];
//   for (let row = 0; row < GRID_ROW_LENGTH; row++) {
//     const currentRow = [];
//     for (let col = 0; col < GRID_COL_LENGTH; col++) {
//       currentRow.push("");
//     }
//     grid.push(currentRow);
//   }

//   console.log(grid);

  const onFormSubmitHandler = async (event) => {
    event.preventDefault();
    console.log(event.target.move.value);
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
    try {
      await props.connection.invoke("SendMove", move);
      // 0 virsus 1 desine 2 apacia 3 kaire
    } catch (e) {
      console.log(e);
    }
  };
  console.log(gameCtx.map);
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
      </div>

      <div className={classes.divcolumn}>
        <h1>Your nickname: {gameCtx.name}</h1>
        <div className={classes.grid}>
          {gameCtx.map.map((row, rowId) => {
            return (
              <div key={rowId} className={classes.row}>
                {row.map((node, nodeId) => {
                  return <Tile key={nodeId} row={rowId} node={nodeId} obstacle={row.obstacle}/>;
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
              {player.turn && <b>{player.name}</b>}
              {!player.turn && <p>{player.name}</p>}
            </div>
          );
        })}
      </div>
    </div>
  );
};

export default Board;
