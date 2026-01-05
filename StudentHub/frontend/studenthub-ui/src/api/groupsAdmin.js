import api from "./axios";

export const getGroups = async () => {
  const res = await api.get("/groups");
  return res.data;
};

export const createGroup = async (name) => {
  const res = await api.post("/groups", { name });
  return res.data;
};

export const assignTeacherToGroup = async (groupId, teacherId) => {
  await api.post("/groups/assign-teacher", {
    groupId,
    teacherId
  });
};

export const addStudentToGroup = async (groupId, studentId) => {
  await api.post("/groups/add-student", {
    groupId,
    studentId
  });
};
