import api from "./axios";

export const getAllCourses = async () => {
  const res = await api.get("/courses");
  return res.data;
};

export const createCourse = async (data) => {
  const res = await api.post("/courses", data);
  return res.data;
};

export const deleteCourse = async (id) => {
  await api.delete(`/courses/${id}`);
};