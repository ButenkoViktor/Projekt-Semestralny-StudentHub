import { getToken } from "./authService";

const API_URL = "https://localhost:7091/api";

function authHeaders() {
  return {
    Authorization: `Bearer ${getToken()}`,
    "Content-Type": "application/json"
  };
}

export async function getMyCourses() {
  const res = await fetch(`${API_URL}/Courses/my`, {
    headers: authHeaders()
  });

  if (!res.ok) {
    throw new Error("Failed to load courses");
  }

  return res.json();
}