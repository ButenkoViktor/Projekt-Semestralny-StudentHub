import { getToken } from "./authService";

const API_URL = "https://localhost:7091/api";

function authHeaders() {
  return {
    Authorization: `Bearer ${getToken()}`,
    "Content-Type": "application/json"
  };
}

export async function getTeacherProfile() {
  const res = await fetch(`${API_URL}/User/me`, {
    headers: authHeaders()
  });
  if (!res.ok) throw new Error("Failed to load profile");
  return res.json();
}

export async function getTeacherCourses() {
  const res = await fetch(`${API_URL}/Courses`, {
    headers: authHeaders()
  });
  if (!res.ok) throw new Error("Failed to load courses");
  return res.json();
}

export async function getTeacherTasks() {
  const res = await fetch(`${API_URL}/Tasks`, {
    headers: authHeaders()
  });
  if (!res.ok) throw new Error("Failed to load tasks");
  return res.json();
}

export async function getTeacherSchedule() {
  const res = await fetch(`${API_URL}/Schedule`, {
    headers: authHeaders()
  });
  if (!res.ok) throw new Error("Failed to load schedule");
  return res.json();
}

export async function getTeacherGroups() {
  const res = await fetch(`${API_URL}/Groups`, {
    headers: authHeaders()
  });
  if (!res.ok) throw new Error("Failed to load groups");
  return res.json();
}