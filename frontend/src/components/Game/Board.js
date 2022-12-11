import React, { useContext } from "react";
import Tile from "./Tile";

import classes from "./Board.module.css";
import GameContext from "../../store/game-context";

const Board = (props) => {
  const gameCtx = useContext(GameContext);

  const findCurrentUnit = () => {
    switch (gameCtx.currentUnit) {
      case 0:
        return "tank";
      case 1:
        return "heli";
      default:
        return "";
    }
  };

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
      case "slup":
        move = 8;
        break;
      case "slright":
        move = 9;
        break;
      case "sldown":
        move = 10;
        break;
      case "slleft":
        move = 11;
        break;
      default:
        move = -1;
        break;
    }
    console.log(move);
    try {
      if (move < 4) {
        await props.connection.invoke("SendMove", move, 1);
      } else {
        const power = move / 4;
        const direction = move % 4;
        debugger;
        power === 1
          ? await props.connection.invoke("ShortShooting")
          : await props.connection.invoke("LongShooting");
        await props.connection.invoke("Shoot", direction);
      }
    } catch (e) {
      console.log(e);
    }
  };

  const undoHandler = async (event) => {
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
  };

  const onCommandSubmitHandler = async (event) => {
    event.preventDefault();

    try {
      await props.connection.invoke("Interpret", event.target.command.value);
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
            Choose a move for <b>{findCurrentUnit()}</b>:
          </label>
          <select id="move" name="move">
            <option value="up">Move Up (Cost: 1)</option>
            <option value="left">Move Left (Cost: 1)</option>
            <option value="right">Move Right (Cost: 1)</option>
            <option value="down">Move Down (Cost: 1)</option>
            <option value="sup">
              Shoot Up. Range: Short, Damage: 2 (Cost: end)
            </option>
            <option value="sleft">
              Shoot Left. Range: Short, Damage: 2 (Cost: end)
            </option>
            <option value="sright">
              Shoot Right. Range: Short, Damage: 2 (Cost: end)
            </option>
            <option value="sdown">
              Shoot Down. Range: Short, Damage: 2 (Cost: end)
            </option>
            <option value="slup">
              Shoot Up. Range: Long, Damage: 1 (Cost: end)
            </option>
            <option value="slleft">
              Shoot Left. Range: Long, Damage: 1 (Cost: end)
            </option>
            <option value="slright">
              Shoot Right. Range: Long, Damage: 1 (Cost: end)
            </option>
            <option value="sldown">
              Shoot Down. Range: Long, Damage: 1 (Cost: end)
            </option>
          </select>
          <input type="submit" value="Submit" disabled={!gameCtx.isMyTurn} />
        </form>
        <form onSubmit={onCommandSubmitHandler}>
          <input type="text" id="command" name="command" />
          <input type="submit" value="Submit" disabled={!gameCtx.isMyTurn} />
          <p style={{ color: "red" }}>
            {gameCtx.isInvalidCommand ? "Neteisinga komanda" : ""}
          </p>
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
          <input
            className={classes.test}
            type="submit"
            value="Submit"
            disabled={!gameCtx.isMyTurn}
          />
        </form>
      </div>

      <div className={classes.divcolumn}>
        <h1>Your nickname: {gameCtx.name}</h1>
        <div className={classes.grid}>
          {gameCtx.map.map((row, rowId) => {
            return (
              <div key={`Row: ${rowId}`} className={classes.row}>
                {row.map((node, nodeId) => {
                  return (
                    <Tile
                      key={`Node: ${rowId} ${nodeId}`}
                      row={rowId}
                      node={nodeId}
                      obstacle={node.isObstacle}
                      unit={node.unit}
                      pickup={node.pickup}
                    />
                  );
                })}
              </div>
            );
          })}
        </div>
      </div>

      <div className={classes.divcolumnsm}>
        <h1>Players</h1>
        <p>
          <b>Moves left:</b> {gameCtx.movesLeft}
        </p>
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
        <h1>Pickups</h1>
        <p>
          <b>Pickups left:</b> {gameCtx.pickupsLeft}
        </p>
      </div>
    </div>
  );
};

export default Board;
