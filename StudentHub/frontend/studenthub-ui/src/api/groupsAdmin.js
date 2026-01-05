import api from "./axios";

export const getGroups = async () => {
  const res = await api.get("/admin/groups");
  return res.data;
};

export const createGroup = async (name) => {
  const res = await api.post("/admin/groups", { name });
  return res.data;
};

export const addStudentToGroup = async (groupId, studentId) => {
  await api.post("/admin/groups/assign-students", {
    groupId,
    studentIds: [studentId],
  });
};

export const assignTeacherToGroup = async (groupId, teacherId) => {
  await api.post("/admin/groups/assign-teacher", {
    groupId,
    teacherId,
  });
};
