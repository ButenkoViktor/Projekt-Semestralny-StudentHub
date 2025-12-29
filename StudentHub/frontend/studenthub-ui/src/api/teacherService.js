import axios from "./axios";

export const getTeacherProfile = async () => {
  const res = await axios.get("/User/me");
  return res.data;
};

export const getTeacherCourses = async () => {
  const res = await axios.get("/Courses");
  return res.data;
};

export const getTeacherTasks = async () => {
  const res = await axios.get("/Tasks");
  return res.data;
};

export const getTeacherSchedule = async () => {
  const res = await axios.get("/Schedule");
  return res.data;
};

export const getTeacherGroups = async () => {
  const res = await axios.get("/teacher/groups");
  return res.data;
};