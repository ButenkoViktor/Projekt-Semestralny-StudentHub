import { HubConnectionBuilder } from "@microsoft/signalr";
import { getToken } from "../api/authService";
import { useEffect, useRef } from "react";

export function useChatHub(onMessage) {
  const connectionRef = useRef(null);

  useEffect(() => {
    const conn = new HubConnectionBuilder()
      .withUrl("https://localhost:7091/hubs/chat", {
        accessTokenFactory: () => getToken()
      })
      .withAutomaticReconnect()
      .build();

    conn.on("ReceiveMessage", onMessage);

    conn.start();
    connectionRef.current = conn;

    return () => {
      conn.stop();
    };
  }, []);

  return connectionRef;
}