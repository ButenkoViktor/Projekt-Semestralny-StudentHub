import api from "./axios";

export const getMyNotes = async () => {
  const res = await api.get("/notes");
  return res.data;
};

export const createNote = async (data) => {
  const res = await api.post("/notes", data);
  return res.data;
};

export const updateNote = async (id, data) => {
  const res = await api.put(`/notes/${id}`, data);
  return res.data;
};

export const deleteNote = async (id) => {
  await api.delete(`/notes/${id}`);
};
