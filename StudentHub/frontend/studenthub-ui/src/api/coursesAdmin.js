import api from "./axios";

export const assignGroupToCourse = async (courseId, groupId) => {
  await api.post("/admin/courses/assign-group", {
    courseId,
    groupId
  });
};