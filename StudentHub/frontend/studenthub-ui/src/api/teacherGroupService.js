import api from "./axios";

export const teacherGroupService = {
  async getMyGroups() {
    const res = await api.get("/teacher/groups");
    return Array.isArray(res.data) ? res.data : [];
  },

  async getGroupStudents(groupId, courseId) {
    const res = await api.get(
      `/teacher/groups/${groupId}/students`,
      { params: { courseId } }
    );
    return Array.isArray(res.data) ? res.data : [];
  },

  async saveGrade(payload) {
    return api.post("/teacher/groups/grade", payload);
  },

  async getGradesHistory(groupId, courseId) {
    const res = await api.get(
      `/teacher/groups/${groupId}/courses/${courseId}/grades-history`
    );
    return Array.isArray(res.data) ? res.data : [];
  },

  async clearAllGrades(groupId, courseId) {
    return api.delete(
      `/teacher/groups/${groupId}/courses/${courseId}/grades`
    );
  },

  async clearGradesByDate(groupId, courseId, date) {
    return api.delete(
      `/teacher/groups/${groupId}/courses/${courseId}/grades/by-date`,
      { params: { date } }
    );
  }
};
