import { HubConnectionBuilder } from "@microsoft/signalr";
import { getToken } from "../api/authService";
import { useEffect, useRef } from "react";

export function useChatHub(onMessage, enabled) {
  const connectionRef = useRef(null);

  useEffect(() => {
    if (!enabled) return;

    const conn = new HubConnectionBuilder()
      .withUrl("https://localhost:7091/chat-hub", {
        accessTokenFactory: () => getToken()
      })
      .withAutomaticReconnect()
      .build();

    conn.on("ReceiveMessage", onMessage);

    conn.start().catch(console.error);
    connectionRef.current = conn;

    return () => {
      conn.stop();
    };
  }, [enabled]);

  return connectionRef;
}