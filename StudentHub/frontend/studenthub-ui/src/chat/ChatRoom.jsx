import { useEffect, useState } from "react";
import { getRoom, sendMessage } from "../api/chatService";
import "./chat.css";

export default function ChatRoom({ room, onBack }) {
  const [messages, setMessages] = useState([]);
  const [text, setText] = useState("");

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
        <button onClick={onBack}>←</button>
        <span>{room.otherUserName}</span>
      </div>

      <div className="chat-messages">
        {messages.map(m => (
          <div
            key={m.id}
            className={`chat-msg ${
              m.senderId === room.otherUserId ? "incoming" : "outgoing"
            }`}
          >
            {m.content}
          </div>
        ))}
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