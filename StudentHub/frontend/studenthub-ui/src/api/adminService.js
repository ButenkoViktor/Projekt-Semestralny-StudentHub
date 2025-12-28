import axios from "./axios";

export const getAdminDashboard = async () => {
  const res = await axios.get("/admin/dashboard");
  return res.data;
};
