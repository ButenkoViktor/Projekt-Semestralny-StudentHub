import { getToken } from "./authService";

const API = "https://localhost:7091/api/chat";

function headers() {
  return {
    Authorization: `Bearer ${getToken()}`,
    "Content-Type": "application/json"
  };
}

export async function searchUsers(query) {
  const res = await fetch(
    `${API}/search?q=${encodeURIComponent(query)}`,
    { headers: headers() }
  );
  if (!res.ok) throw new Error("Search failed");
  return res.json();
}

export async function getRooms() {
  const res = await fetch(`${API}/rooms`, { headers: headers() });
  if (!res.ok) throw new Error("Rooms failed");
  return res.json();
}

export async function getRoom(id) {
  const res = await fetch(`${API}/room/${id}`, { headers: headers() });
  if (!res.ok) throw new Error("Room load failed");
  return res.json();
}

export async function createOrGetRoom(userId) {
  const res = await fetch(`${API}/room`, {
    method: "POST",
    headers: headers(),
    body: JSON.stringify({ targetUserId: userId })
  });
  if (!res.ok) throw new Error("Room create failed");
  return res.json();
}

export async function sendMessage(roomId, content) {
  const res = await fetch(`${API}/send`, {
    method: "POST",
    headers: headers(),
    body: JSON.stringify({ roomId, content })
  });
  if (!res.ok) throw new Error("Send failed");
  return res.json();
}