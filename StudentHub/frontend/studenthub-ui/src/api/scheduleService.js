import axios from "./axios";

export const getSchedule = async () => {
  const res = await axios.get("/schedule");
  return res.data;
};

export const createSchedule = async (data) => {
  const res = await axios.post("/schedule", data);
  return res.data;
};

export const deleteSchedule = async (id) => {
  await axios.delete(`/schedule/${id}`);
};