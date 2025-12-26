import { useEffect, useState } from "react";
import { getRoom, sendMessage } from "../api/chatService";
import { usePresence } from "./PresenceContext";
import "./chat.css";

function formatTime(dateString) {
  const d = new Date(dateString);
  if (isNaN(d)) return "";
  return d.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" });
}

export default function ChatRoom({ room, onBack }) {
  const [messages, setMessages] = useState([]);
  const [text, setText] = useState("");
  const { onlineUsers } = usePresence();
  const isOnline = onlineUsers.has(room.otherUserId);

  useEffect(() => {
    getRoom(room.id).then(r => setMessages(r.messages || []));
  }, [room.id]);

  async function send() {
    if (!text.trim()) return;
    const msg = await sendMessage(room.id, text);
    setMessages(m => [...m, msg]);
    setText("");
  }

  return (
    <>
      <div className="chat-header">
        <button className="btn-back-room" onClick={onBack}>←</button>
        <span>
          {room.otherUserName}
          <span className={`status-text ${isOnline ? "online" : "offline"}`}>
            {isOnline ? " 🟢" : " 🔴"}
          </span>
        </span>
      </div>

      <div className="chat-messages">
        {messages.map(m => {
          const isIncoming = m.senderId === room.otherUserId;

          return (
            <div
              key={m.id}
              style={{
                display: "flex",
                flexDirection: "column",
                alignItems: isIncoming ? "flex-start" : "flex-end",
                marginBottom: "10px"
              }}
            >
              <div className="msg-author">
                {isIncoming ? room.otherUserName : "You"}
              </div>

              <div className={`chat-msg ${isIncoming ? "incoming" : "outgoing"}`}>
                {m.content}
              </div>

              <div
                className={`msg-time ${isIncoming ? "incoming" : "outgoing"}`}
              >
                {formatTime(m.sentAt)}
              </div>
            </div>
          );
        })}
      </div>

      <div className="chat-input">
        <input
          value={text}
          onChange={e => setText(e.target.value)}
          placeholder="Type message..."
          onKeyDown={e => e.key === "Enter" && send()}
        />
        <button onClick={send}>Send</button>
      </div>
    </>
  );
}