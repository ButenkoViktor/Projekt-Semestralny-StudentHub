import axios from "./axios";

export const getTeacherTasks = async () => {
  const res = await axios.get("/tasks/teacher");
  return res.data;
};

export const createTask = async (data) => {
  const res = await axios.post("/tasks", data);
  return res.data;
};

export const deleteTask = async (id) => {
  await axios.delete(`/tasks/${id}`);
};

export const getTaskSubmissions = async (taskId) => {
  const res = await axios.get(`/tasks/${taskId}/submissions`);
  return res.data;
};

export const getStudentTasks = async () => {
  const res = await axios.get("/tasks/student");
  return res.data;
};

export const submitTask = async (taskId, data) => {
  const res = await axios.post(`/tasks/${taskId}/submit`, data);
  return res.data;
};

export const updateSubmission = async (taskId, data) => {
  const res = await axios.put(`/tasks/${taskId}/submit`, data);
  return res.data;
};


