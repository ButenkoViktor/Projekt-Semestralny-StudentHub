import api from "./axios";

export const getAllUsers = async () => {
  const res = await api.get("/admin/users");
  return res.data;
};