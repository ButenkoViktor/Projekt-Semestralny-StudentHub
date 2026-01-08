import api from "./axios";

export const getSchedules = async () => {
  const res = await api.get("/schedule");
  return res.data;
};

export const createSchedule = async (data) => {
  const res = await api.post("/schedule", data);
  return res.data;
};

export const deleteSchedule = async (id) => {
  await api.delete(`/schedule/${id}`);
};
