import axios from "./axios";

export const getGroupStudents = async (groupId, courseId, date) => {
  const res = await axios.get(
    `/teacher/groups/${groupId}/students`,
    {
      params: { courseId, date }
    }
  );
  return res.data;
};

export const saveGrade = async (payload) => {
  await axios.post("/teacher/groups/grade", payload);
};

export const getGradesHistory = async (groupId, courseId) => {
  const res = await axios.get(
    `/teacher/groups/${groupId}/grades-history`,
    {
      params: { courseId }
    }
  );
  return res.data;
};
