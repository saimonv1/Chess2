import { useState, useEffect } from "react";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

import Layout from "./components/Layout/Layout";
import Board from "./components/Game/Board";

function App() {
  const [connection, setConnection] = useState();
  const [message, setMessage] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  useEffect(() => {
    setupConnection("testuser", "testroom");
  }, []);

  const setupConnection = async (user, room) => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:7001/chat")
        .configureLogging(LogLevel.Information)
        .build();

      connection.on("ReceiveMessage", (user, message) => {
        setMessage(message);
        console.log(message);
      });

      await connection
        .start()
        .then(() => console.log("Connection started"))
        .catch((e) => setErrorMessage(`${e}`));
      await connection.invoke("JoinRoom", { user, room });
      console.log("connection start");
      setConnection(connection);
    } catch (e) {
      console.log(e);
    }
  };

  const sendMessageHandler = async (event) => {
    try {
      await connection.invoke("SendMessage", "test", "yes");
    } catch (e) {
      console.log(e);
    }
  };

  return (
    <Layout>
      <div>
        {errorMessage && <h1>{errorMessage}</h1>}
        <Board />
      </div>
    </Layout>
  );
}

export default App;
