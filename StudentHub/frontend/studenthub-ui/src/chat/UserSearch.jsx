import { useEffect, useState } from "react";
import { searchUsers, createOrGetRoom } from "../api/chatService";

export default function UserSearch({ onOpen }) {
  const [query, setQuery] = useState("");
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (query.trim().length < 2) {
      setUsers([]);
      return;
    }

    const timeout = setTimeout(async () => {
      try {
        setLoading(true);
        const data = await searchUsers(query);
        setUsers(data);
      } catch (e) {
        console.error(e);
      } finally {
        setLoading(false);
      }
    }, 400); 

    return () => clearTimeout(timeout);
  }, [query]);

  async function openChat(userId) {
    const room = await createOrGetRoom(userId);
    onOpen(room);
    setUsers([]);
    setQuery("");
  }

  return (
    <div className="chat-search">
      <input
        placeholder="Search by name or email"
        value={query}
        onChange={e => setQuery(e.target.value)}
      />

      {loading && <div className="chat-search-loading">Searching...</div>}

      {users.length > 0 && (
        <div className="chat-search-results">
          {users.map(u => (
            <div
              key={u.id}
              className="chat-search-item"
              onClick={() => openChat(u.id)}
            >
              <strong>{u.fullName}</strong>
              <div className="chat-search-email">{u.email}</div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}