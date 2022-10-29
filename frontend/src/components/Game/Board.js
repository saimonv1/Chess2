import React, { useContext } from "react";
import Tile from "./Tile";

import classes from "./Board.module.css";
import GameContext from "../../store/game-context";

const Board = (props) => {
  const gameCtx = useContext(GameContext);

  const findCurrentUnit = () => {
    switch(gameCtx.currentUnit) {
      case 0:
        return "tank";
      case 1:
        return "heli";
      default:
        return "";
    }
  }

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
      case "down":
        move = 2;
        break;
      case "left":
        move = 3;
        break;
      case "sup":
        move = 4;
        break;
      case "sright":
        move = 5;
        break;
      case "sdown":
        move = 6;
        break;
      case "sleft":
        move = 7;
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


  const onMapChangeSubmitHandler = async (event) => {
    event.preventDefault();

    try {
      await props.connection.invoke("MapChange", event.target.mapChange.value);
    } catch (e) {
      console.log(e);
    }
  }

  return (
    <div className={classes.divrow}>
      <div className={classes.divcolumnsm}>
        <h1>Controls</h1>
        <p><b>Moves left:</b> {gameCtx.movesLeft}</p>
        <form onSubmit={onFormSubmitHandler}>
          <label style={{ display: "block" }} htmlFor="move">
            Choose a move for <b>{findCurrentUnit()}</b>:
          </label>
          <select id="move" name="move">
            <option value="up">Move Up (Cost: 1)</option>
            <option value="left">Move Left (Cost: 1)</option>
            <option value="right">Move Right (Cost: 1)</option>
            <option value="down">Move Down (Cost: 1)</option>
            <option value="sup">Shoot Up (Cost: end)</option>
            <option value="sleft">Shoot Left (Cost: end)</option>
            <option value="sright">Shoot Right (Cost: end)</option>
            <option value="sdown">Shoot Down (Cost: end)</option>
          </select>
          <input type="submit" value="Submit" disabled={!gameCtx.isMyTurn}/>
        </form>
        <button onClick={undoHandler}>Undo</button>

        <form onSubmit={onMapChangeSubmitHandler}>
          <label style={{ display: "block" }} htmlFor="mapChange">
            Change a map:
          </label>
          <select id="mapChange" name="mapChange">
            <option value="0">Empty</option>
            <option value="1">Plus</option>
            <option value="2">O</option>
            <option value="3">Random</option>
          </select>
          <input className={classes.test} type="submit" value="Submit" disabled={!gameCtx.isMyTurn}/>
        </form>
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
