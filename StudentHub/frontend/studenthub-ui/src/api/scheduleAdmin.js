import api from "./axios";

export const getSchedule = async () => {
  const res = await api.get("/schedule");
  return res.data;
};

export const createSchedule = async (data) => {
  return api.post("/schedule", {
    courseId: data.courseId,
    teacherName: data.teacherName,
    startTime: data.startTime,
    endTime: data.endTime,
    groupId: data.groupId,
    lessonType: data.lessonType
  });
};

export const deleteSchedule = async (id) => {
  return api.delete(`/schedule/${id}`);
};
