import { useState } from "react";
import ChatSidebar from "./ChatSidebar";
import ChatRoom from "./ChatRoom";
import "./chat.css";

export default function ChatWindow({ onClose }) {
  const [activeRoom, setActiveRoom] = useState(null);

  return (
    <div className="chat-window">
      {!activeRoom ? (
        <>
          <div className="chat-header">
            <span className="chat-header-title">Chats</span>
            <button className="btn-close" onClick={onClose}>✖</button>
          </div>
          <ChatSidebar onSelectRoom={setActiveRoom} />
        </>
      ) : (
        <ChatRoom room={activeRoom} onBack={() => setActiveRoom(null)} />
      )}
    </div>
  );
}