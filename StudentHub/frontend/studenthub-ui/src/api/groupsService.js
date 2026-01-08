import api from "./axios";

export const getMyGroups = async () => {
  const res = await api.get("/groups/my");
  return res.data;
};