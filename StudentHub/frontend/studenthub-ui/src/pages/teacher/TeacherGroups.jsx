import { useEffect, useState } from "react";
import { teacherGroupService } from "../../api/teacherGroupService";
import "./TeacherGroups.css";

export default function TeacherGroups() {
  const [groups, setGroups] = useState([]);
  const [selectedGroup, setSelectedGroup] = useState(null);
  const [students, setStudents] = useState([]);
  const [history, setHistory] = useState([]);
  const [showHistory, setShowHistory] = useState(false);

  const [selectedDate, setSelectedDate] = useState(
    new Date().toISOString().split("T")[0]
  );

  useEffect(() => {
    loadGroups();
  }, []);

  const loadGroups = async () => {
    const data = await teacherGroupService.getMyGroups();
    setGroups(data);
  };

  const openGroup = async (group) => {
    setSelectedGroup(group);
    setShowHistory(false);

    const data = await teacherGroupService.getGroupStudents(
      group.groupId,
      group.courseId
    );

    setStudents(
      data.map(s => ({
        ...s,
        isPresent: false,
        grade: ""
      }))
    );
  };

  const updateStudent = (index, field, value) => {
    const updated = [...students];
    updated[index] = { ...updated[index], [field]: value };
    setStudents(updated);
  };

  const saveStudent = async (student) => {
    await teacherGroupService.saveGrade({
      studentId: student.studentId,
      groupId: selectedGroup.groupId,
      courseId: selectedGroup.courseId,
      date: selectedDate,
      grade: student.grade ? Number(student.grade) : null,
      isPresent: student.isPresent
    });

    alert("Saved");
  };

  const loadHistory = async () => {
    const data = await teacherGroupService.getGradesHistory(
      selectedGroup.groupId,
      selectedGroup.courseId
    );
    setHistory(data);
    setShowHistory(true);
  };

  const clearAllHistory = async () => {
    if (!window.confirm("Delete ALL grades forever?")) return;

    await teacherGroupService.clearAllGrades(
      selectedGroup.groupId,
      selectedGroup.courseId
    );

    setHistory([]);
    alert("All grades deleted");
  };

  const clearHistoryByDate = async () => {
    if (!window.confirm("Delete grades for selected date?")) return;

    await teacherGroupService.clearGradesByDate(
      selectedGroup.groupId,
      selectedGroup.courseId,
      selectedDate
    );

    setHistory(history.filter(
      h => h.date.split("T")[0] !== selectedDate
    ));

    alert("Grades deleted for selected date");
  };

  return (
    <div className="teacher-groups">
      <section className="welcome-card">
        <h1>My Teaching Groups</h1>
        <p>
          Manage attendance and grades. All changes are saved permanently.
        </p>
      </section>

      <div className="groups-list">
        {groups.map(g => (
          <div
            key={`${g.groupId}-${g.courseId}`}
            className={`group-card ${
              selectedGroup?.groupId === g.groupId ? "active" : ""
            }`}
            onClick={() => openGroup(g)}
          >
            <h3>{g.groupName}</h3>
            <span>{g.courseTitle}</span>
          </div>
        ))}
      </div>

      {selectedGroup && (
        <div className="students-section">
          <div className="students-header">
            <h2>
              {selectedGroup.groupName} — {selectedGroup.courseTitle}
            </h2>

            <div className="actions">
              <input
                type="date"
                value={selectedDate}
                onChange={e => setSelectedDate(e.target.value)}
              />

              <button className="secondary" onClick={loadHistory}>
                History
              </button>

              <button className="danger" onClick={clearHistoryByDate}>
                Clear by Date
              </button>

              <button className="danger" onClick={clearAllHistory}>
                Clear All
              </button>
            </div>
          </div>

          {!showHistory && (
            <table>
              <thead>
                <tr>
                  <th>Student</th>
                  <th>Present</th>
                  <th>Grade</th>
                  <th>Save</th>
                </tr>
              </thead>
              <tbody>
                {students.map((s, i) => (
                  <tr key={s.studentId}>
                    <td>{s.studentName}</td>
                    <td>
                      <input
                        type="checkbox"
                        checked={s.isPresent}
                        onChange={e =>
                          updateStudent(i, "isPresent", e.target.checked)
                        }
                      />
                    </td>
                    <td>
                      <input
                        type="number"
                        min="1"
                        max="5"
                        disabled={!s.isPresent}
                        value={s.grade}
                        onChange={e =>
                          updateStudent(i, "grade", e.target.value)
                        }
                      />
                    </td>
                    <td>
                      <button onClick={() => saveStudent(s)}>
                        Save
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}

          {showHistory && (
            <table className="history-table">
              <thead>
                <tr>
                  <th>Date</th>
                  <th>Student</th>
                  <th>Present</th>
                  <th>Grade</th>
                </tr>
              </thead>
              <tbody>
                {history.map((h, i) => (
                  <tr key={i}>
                    <td>{h.date.split("T")[0]}</td>
                    <td>{h.studentName}</td>
                    <td>{h.isPresent ? "✔" : "✖"}</td>
                    <td>{h.grade ?? "-"}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </div>
      )}
    </div>
  );
}
