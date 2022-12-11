import { useState, useEffect, useContext } from "react";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

import Layout from "./components/Layout/Layout";
import Board from "./components/Game/Board";
import User from "./components/Game/User";
import Lobby from "./components/Game/Lobby";
import GameContext from "./store/game-context";


function App() {
  const [connection, setConnection] = useState();

  const [isLoading, setIsLoading] = useState(true);
  const [connectionErrorMessage, setConnectionErrorMessage] = useState("");

  const [userNameInputError, setUserNameInputError] = useState("");

  const gameCtx = useContext(GameContext);

  const userName = gameCtx.name;

  useEffect(() => {
    setupConnection();
    // eslint-disable-next-line
  }, []);

  const setupConnection = async () => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:7001/game")
        .configureLogging(LogLevel.Information)
        .build();

      connection.on("ConfirmUsername", (users, message, user) => {
        if (message) {
          setUserNameInputError(message);
        } else {
          gameCtx.changeName(user.name);
          gameCtx.changeColor(user.color);
          users.forEach((user) => {
            gameCtx.addPlayer(user);
          });
        }
      });

      connection.on("PlayerJoin", (player) => {
        gameCtx.addPlayer(player);
      });

      connection.on("PlayerLeave", (player) => {
        gameCtx.removePlayer(player);
      });

      connection.on("ReadyStatus", (connectionId, status) => {
        gameCtx.changeReadyStatus(connectionId, status);
      });

      connection.on("GameStatus", (status) => {
        gameCtx.changeGameStatus(status);
      });

      connection.on("Map", (map) => {
        gameCtx.insertMap(map);
      });

      connection.on("FirstTurn", (connectionId) => {
        gameCtx.changeTurnStatus(null, connectionId);
      });

      connection.on("NextTurn", (currentConnectionId, nextConnectionId) => {
        gameCtx.changeTurnStatus(currentConnectionId, nextConnectionId);
      });

      connection.on("IllegalTurn", () => {

      });

      connection.on("MoveItem", (oldX, oldY, newX, newY) => {
        gameCtx.gameMove(oldX, oldY, newX, newY);
      });

      connection.on("MovesUpdate", (newMoves) => {
        gameCtx.setMovesLeft(newMoves);
      });

      connection.on("PickupsUpdate", (newPickups) => {
        gameCtx.setPickupsLeft(newPickups);
      });

      connection.on("InvalidCommand", (state) => {
        gameCtx.changeInvalidCommand(state);
      });

      await connection
        .start()
        .then(() => {
          console.log("Connection started");
          setIsLoading(false);
        })
        .catch((e) => {
          setConnectionErrorMessage(`${e}`);
          setIsLoading(false);
        });
      setConnection(connection);
    } catch (e) {
      console.log(e);
    }
  };

  return (
    <Layout>
      <div>
        {isLoading ? (
          <h1>Loading...</h1>
        ) : connectionErrorMessage ? (
          <h1 style={{ color: "red" }}>{connectionErrorMessage}</h1>
        ) : !userName ? (
          <User
            error={userNameInputError}
            setError={setUserNameInputError}
            connection={connection}
          />
        ) : gameCtx.gameStatus ? (
          <Board connection={connection} />
        ) : (
          <Lobby connection={connection} />
        )}
      </div>
    </Layout>
  );
}

export default App;
