import { useEffect, useState } from "react";
import { getRooms } from "../api/chatService";
import UserSearch from "./UserSearch";
import { usePresence } from "../chat/PresenceContext";

export default function ChatSidebar({ onSelectRoom }) {
  const [rooms, setRooms] = useState([]);
  const { onlineUsers } = usePresence();

  useEffect(() => {
    getRooms().then(setRooms);
  }, []);

  return (
    <div className="chat-sidebar">
      <UserSearch onOpen={onSelectRoom} />

      <div className="chat-list">
        {rooms.map(r => {
          const isOnline = onlineUsers.has(r.otherUserId);

          return (
            <div
              key={r.id}
              className="chat-list-item"
              onClick={() => onSelectRoom(r)}
            >
              <div className="avatar-wrapper">
                <div className="avatar">
                  {r.otherUserName[0]}
                </div>

                <span
                  className={`presence-dot ${isOnline ? "online" : "offline"}`}
                />
              </div>

              <span>{r.otherUserName}</span>
            </div>
          );
        })}
      </div>
    </div>
  );
}