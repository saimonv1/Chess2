import { useState, useEffect } from "react";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

import Header from "./components/Layout/Header";

function App() {
  const [connection, setConnection] = useState();
  const [message, setMessage] = useState('');

  useEffect(() => {
    joinRoom("testuser", "testroom");
  }, []);

  const joinRoom = async (user, room) => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:7001/chat")
        .configureLogging(LogLevel.Information)
        .build();

      connection.on("ReceiveMessage", (user, message) => {
        setMessage(message);
        console.log(message);
      });

      await connection.start().then(() => console.log('Connection started')).catch(e => console.log('error:' + e));
      await connection.invoke("JoinRoom", { user, room });
      console.log("connection start");
      setConnection(connection);
    } catch (e) {
      console.log(e);
    }
  };

  const buttonClickHandler = async (event) => {
    try {
      await connection.invoke("SendMessage", "test", "yes");
    } catch (e) {
      console.log(e);
    }
  };

  return (
    <div>
      <Header />
      <h1>HELLO WORLD</h1>
      <button onClick={buttonClickHandler}>Send message</button>
      <p>{message}</p>
    </div>
  );
}

export default App;
