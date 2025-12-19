import { jwtDecode } from "jwt-decode";

const API_URL = "https://localhost:7091/api/Auth";

export async function loginRequest(email, password) {
  const res = await fetch(`${API_URL}/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password })
  });

  if (!res.ok) return { success: false };

  const data = await res.json();
  localStorage.setItem("token", data.token);

  return { success: true };
}

export async function registerRequest(form) {
  const res = await fetch(`${API_URL}/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(form)
  });

  return res;
}

export function getToken() {
  return localStorage.getItem("token");
}

export function getRoles() {
  const token = getToken();
  if (!token) return [];

  const decoded = jwtDecode(token);

  const roles =
    decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

  return Array.isArray(roles) ? roles : [roles];
}