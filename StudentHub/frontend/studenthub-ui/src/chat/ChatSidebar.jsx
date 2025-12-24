import { useEffect, useState } from "react";
import { getRooms } from "../api/chatService";
import UserSearch from "./UserSearch";

export default function ChatSidebar({ onSelectRoom }) {
  const [rooms, setRooms] = useState([]);

  useEffect(() => {
    getRooms().then(setRooms);
  }, []);

  return (
    <div className="chat-sidebar">
      <UserSearch onOpen={onSelectRoom} />

      <div className="chat-list">
        {rooms.map(r => (
          <div
            key={r.id}
            className="chat-list-item"
            onClick={() => onSelectRoom(r)}
          >
            <div className="avatar">{r.otherUserName[0]}</div>
            <span>{r.otherUserName}</span>
          </div>
        ))}
      </div>
    </div>
  );
}