import axios from "./axios";

export async function getTeacherTasks() {
  const res = await axios.get("/tasks/teacher");
  return res.data;
}

export async function createTask(data) {
  const res = await axios.post("/tasks", data);
  return res.data;
}

export async function updateTask(id, data) {
  const res = await axios.put(`/tasks/${id}`, data);
  return res.data;
}

export async function deleteTask(id) {
  await axios.delete(`/tasks/${id}`);
}

export async function getTaskSubmissions(taskId) {
  const res = await axios.get(`/tasks/${taskId}/submissions`);
  return res.data;
}