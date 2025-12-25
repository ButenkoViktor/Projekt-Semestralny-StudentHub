import { createContext, useContext, useEffect, useState } from "react";
import { startPresenceConnection } from "./presenceConnection";

const PresenceContext = createContext();

export function PresenceProvider({ children }) {
  const [onlineUsers, setOnlineUsers] = useState(new Set());

  useEffect(() => {
    startPresenceConnection(
      userId =>
        setOnlineUsers(prev => new Set(prev).add(userId)),
      userId =>
        setOnlineUsers(prev => {
          const next = new Set(prev);
          next.delete(userId);
          return next;
        })
    );
  }, []);

  return (
    <PresenceContext.Provider value={{ onlineUsers }}>
      {children}
    </PresenceContext.Provider>
  );
}

export function usePresence() {
  return useContext(PresenceContext);
}