import { useEffect, useState } from "react";
import {
  getMyGroups,
  getGroupStudents
} from "../../api/teacherGroupService";
import GroupStudentsTable from "../../pages/teacher/GroupStudentsTable";
import "./TeacherGroups.css";


export default function TeacherGroupsPage() {
    const USE_MOCK = true; // Set to false to use real API
    const MOCK_GROUPS = [
  {
    groupId: 1,
    groupName: "IPZ-21",
    courseId: 101,
    courseTitle: "Web Development"
  },
  {
    groupId: 2,
    groupName: "CS-32",
    courseId: 102,
    courseTitle: "Databases"
  }
];

const MOCK_STUDENTS = [
  {
    studentId: 1,
    studentName: "Ivan Petrenko",
    grade: null,
    isPresent: false
  },
  {
    studentId: 2,
    studentName: "Olena Kovalenko",
    grade: 4,
    isPresent: true
  }
];
  const [groups, setGroups] = useState([]);
  const [selected, setSelected] = useState(null);
  const [students, setStudents] = useState([]);
  const [loading, setLoading] = useState(false);

 useEffect(() => {
  if (USE_MOCK) {
    setGroups(MOCK_GROUPS);
  } else {
    getMyGroups()
      .then(setGroups)
      .catch(err => console.error("GET GROUPS ERROR", err));
  }
}, []);

 const openGroup = async (g) => {
  setSelected(g);
  setLoading(true);

  if (USE_MOCK) {
    setTimeout(() => {
      setStudents(MOCK_STUDENTS);
      setLoading(false);
    }, 500);
  } else {
    try {
      const data = await getGroupStudents(g.groupId, g.courseId);
      setStudents(data);
    } catch (e) {
      console.error("GET STUDENTS ERROR", e);
    } finally {
      setLoading(false);
    }
  }
};

  return (
    <div className="teacher-page">
      <h1>My Groups</h1>

      <div className="group-list">
        {groups.map(g => (
          <button
            key={`${g.groupId}-${g.courseTitle}`}
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
          <h2>
            {selected.groupName} — {selected.courseTitle}
          </h2>

          {loading ? (
            <p>Loading students...</p>
          ) : (
            <GroupStudentsTable
              students={students}
              groupId={selected.groupId}
              courseId={selected.courseId}
            />
          )}
        </section>
      )}
    </div>
  );
}