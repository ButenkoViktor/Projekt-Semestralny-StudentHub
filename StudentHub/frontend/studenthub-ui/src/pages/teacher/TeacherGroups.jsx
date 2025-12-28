import { useEffect, useState } from "react";
import {
  getMyGroups,
  getGroupStudents
} from "../../api/teacherGroupService";
import GroupStudentsTable from "./GroupStudentsTable";
import "./TeacherGroups.css";

export default function TeacherGroupsPage() {
  const [groups, setGroups] = useState([]);
  const [selected, setSelected] = useState(null);
  const [students, setStudents] = useState([]);
  const [loading, setLoading] = useState(false);

  const [lessonDate, setLessonDate] = useState(
    new Date().toISOString().slice(0, 10)
  );

  useEffect(() => {
    async function loadGroups() {
      try {
        const data = await getMyGroups();
        setGroups(data);
      } catch (e) {
        console.error("LOAD GROUPS ERROR", e);
      }
    }
    loadGroups();
  }, []);

  const openGroup = async (g) => {
    setSelected(g);
    setLoading(true);
    try {
      const data = await getGroupStudents(g.groupId, g.courseId);
      setStudents(data);
    } catch (e) {
      console.error("LOAD STUDENTS ERROR", e);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="teacher-page">
      <h1>My Groups</h1>

      <div className="group-list">
        {groups.map(g => (
          <button
            key={`${g.groupId}-${g.courseId}`}
            onClick={() => openGroup(g)}
            className={selected?.groupId === g.groupId ? "active" : ""}
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

          {loading ? (
            <p>Loading students...</p>
          ) : (
            <GroupStudentsTable
              students={students}
              groupId={selected.groupId}
              courseId={selected.courseId}
              lessonDate={lessonDate}
            />
          )}
        </section>
      )}
    </div>
  );
}