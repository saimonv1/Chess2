import { useState, useEffect, useContext } from "react";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

import Layout from "./components/Layout/Layout";
import Board from "./components/Game/Board";
import User from "./components/Game/User";
import Lobby from "./components/Game/Lobby";
import GameProvider from "./store/GameProvider";
import GameContext from "./store/game-context";

function App() {
  const [connection, setConnection] = useState();

  const [isLoading, setIsLoading] = useState(true);
  const [connectionErrorMessage, setConnectionErrorMessage] = useState("");
  const [userName, setUserName] = useState("");
  const [color, setColor] = useState("");

  const [userNameInputError, setUserNameInputError] = useState("");

  const gameCtx = useContext(GameContext);

  useEffect(() => {
    setupConnection();
  }, []);

  const setupConnection = async () => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:7001/game")
        .configureLogging(LogLevel.Information)
        .build();

      connection.on("ConfirmUsername", (user, message) => {
        console.log({ user, message });
        if (message) {
          setUserNameInputError(message);
        } else {
          setUserName(user.name);
          setColor(user.color);
        }
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
    <GameProvider>
      <Layout>
        <div>
          {isLoading ? (
            <h1>Loading...</h1>
          ) : connectionErrorMessage ? (
            <h1 style={{ color: "red" }}>{connectionErrorMessage}</h1>
          ) : userName ? (
            <Board userName={userName} />
          ) : gameCtx.gameStatus ? (
            <User
              error={userNameInputError}
              setError={setUserNameInputError}
              connection={connection}
            />
          ) : <Lobby />  
          }
        </div>
      </Layout>
    </GameProvider>
  );
}

export default App;
