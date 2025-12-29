import { useEffect, useState } from "react";
import GroupStudentsTable from "./GroupStudentsTable";
import GroupGradesHistoryPage from "./GroupGradesHistoryPage";
import { getTeacherGroups } from "../../api/teacherService";
import { getGroupStudents } from "../../api/teacherGroupService";

export default function TeacherGroupsPage() {
  const [groups, setGroups] = useState([]);
  const [selected, setSelected] = useState(null);
  const [students, setStudents] = useState([]);
  const [activeTab, setActiveTab] = useState("lesson");
  const [lessonDate, setLessonDate] = useState(
    new Date().toISOString().slice(0, 10)
  );
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    loadGroups();
  }, []);

  async function loadGroups() {
    try {
      const data = await getTeacherGroups();
      setGroups(data);
    } catch (e) {
      console.error("Failed to load groups", e);
    }
  }

  async function openGroup(group) {
    setSelected(group);
    setActiveTab("lesson");
    setLoading(true);

    try {
      const data = await getGroupStudents(
        group.groupId,
        group.courseId,
        lessonDate
      );
      setStudents(data);
    } catch (e) {
      console.error("Failed to load students", e);
    } finally {
      setLoading(false);
    }
  }

  return (
    <div className="teacher-page">
      <section className="welcome-card">
        <h1>My Groups</h1>
        <p>Courses assigned to you by administrator</p>
      </section>

      <div className="group-list">
        {groups.map(g => (
          <button
            key={`${g.groupId}-${g.courseId}`}
            className={selected?.groupId === g.groupId ? "active" : ""}
            onClick={() => openGroup(g)}
          >
            <strong>{g.groupName}</strong>
            <span>{g.courseTitle}</span>
          </button>
        ))}
      </div>

      {selected && (
        <section className="group-section">
          <div className="group-header">
            <h2>
              {selected.groupName} — {selected.courseTitle}
            </h2>

            <input
              type="date"
              value={lessonDate}
              onChange={e => setLessonDate(e.target.value)}
            />
          </div>

          <div className="group-tabs">
            <button
              className={activeTab === "lesson" ? "active" : ""}
              onClick={() => setActiveTab("lesson")}
            >
              Lesson
            </button>

            <button
              className={activeTab === "history" ? "active" : ""}
              onClick={() => setActiveTab("history")}
            >
              Grades history
            </button>
          </div>

          {loading && <p>Loading...</p>}

          {!loading && activeTab === "lesson" && (
            <GroupStudentsTable
              students={students}
              setStudents={setStudents}
              groupId={selected.groupId}
              courseId={selected.courseId}
              lessonDate={lessonDate}
            />
          )}

          {!loading && activeTab === "history" && (
            <GroupGradesHistoryPage
              groupId={selected.groupId}
              courseId={selected.courseId}
            />
          )}
        </section>
      )}
    </div>
  );
}
