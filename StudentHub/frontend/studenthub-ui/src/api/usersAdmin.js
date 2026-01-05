import api from "./axios";

export const getTeachers = async () => {
  const res = await api.get("/user/teachers");
  return res.data;
};

export const getStudents = async () => {
  const res = await api.get("/admin/users");
  return res.data.filter(u => u.roles.includes("Student"));
};