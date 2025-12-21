import { getToken } from "./authService";

const API_URL = "https://localhost:7091/api";

function authHeaders() {
  return {
    Authorization: `Bearer ${getToken()}`,
    "Content-Type": "application/json"
  };
}

export async function getCurrentUser() {
  const res = await fetch(`${API_URL}/User/me`, {
    headers: authHeaders()
  });

  if (!res.ok) throw new Error("Failed to load user");

  return res.json();
}

export async function getTodaySchedule() {
  const res = await fetch(`${API_URL}/Schedule`, {
    headers: authHeaders()
  });

  if (!res.ok) throw new Error("Failed to load schedule");

  const data = await res.json();
  const today = new Date().toDateString();

  return data.filter(
    s => new Date(s.startTime).toDateString() === today
  );
}

export async function getUpcomingDeadlines() {
  const res = await fetch(`${API_URL}/Tasks`, {
    headers: authHeaders()
  });

  if (!res.ok) throw new Error("Failed to load tasks");

  const data = await res.json();
  const now = new Date();

  return data
    .filter(t => new Date(t.deadline) > now)
    .sort((a, b) => new Date(a.deadline) - new Date(b.deadline))
    .slice(0, 5);
}

export async function getAnnouncements() {
  const res = await fetch(`${API_URL}/Announcements`, {
    headers: authHeaders()
  });

  if (!res.ok) throw new Error("Failed to load announcements");

  return res.json();
}

export async function getStudyStats() {
  const res = await fetch(`${API_URL}/Tasks`, {
    headers: authHeaders()
  });

  if (!res.ok) throw new Error("Failed to load stats");

  const tasks = await res.json();
  const now = new Date();

  const pendingTasks = tasks.filter(
    t => new Date(t.deadline) > now
  ).length;

  return {
    totalTasks: tasks.length,
    pendingTasks
  };
}