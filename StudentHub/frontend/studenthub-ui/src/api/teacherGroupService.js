import axios from "./axios";

export const getMyGroups = async () => {
  const res = await axios.get("/teacher/groups");
  return res.data;
};

export const getGroupStudents = async (groupId, courseId) => {
  const res = await axios.get(
    `/teacher/groups/${groupId}/course/${courseId}`
  );
  return res.data;
};

export const saveGrade = async (payload) => {
  await axios.post("/teacher/groups/grade", payload);
};

export const getGradesHistory = async (groupId, courseId) => {
  const res = await axios.get(
    `/teacher/groups/${groupId}/courses/${courseId}/grades-history`
  );
  return res.data;
};

export const getFinalGrades = async (groupId, courseId) => {
  const res = await axios.get(
    `/teacher/groups/${groupId}/courses/${courseId}/final-grades`
  );
  return res.data;
};