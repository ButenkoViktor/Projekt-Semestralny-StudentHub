import { saveGrade } from "../../api/teacherGroupService";

export default function GroupStudentsTable({
  students,
  setStudents,
  groupId,
  courseId,
  lessonDate
}) {
  const update = (id, patch) => {
    setStudents(prev =>
      prev.map(s =>
        s.studentId === id ? { ...s, ...patch } : s
      )
    );
  };

  const saveAll = async () => {
    for (const s of students) {
      await saveGrade({
        studentId: s.studentId,
        groupId,
        courseId,
        date: lessonDate,
        isPresent: s.isPresent,
        grade: s.grade
      });
    }
    alert("Saved successfully");
  };

  return (
    <>
      <table className="students-table">
        <thead>
          <tr>
            <th>Student</th>
            <th>Present</th>
            <th>Grade</th>
          </tr>
        </thead>
        <tbody>
          {students.map(s => (
            <tr key={s.studentId}>
              <td>{s.studentName}</td>
              <td>
                <input
                  type="checkbox"
                  checked={s.isPresent}
                  onChange={e =>
                    update(s.studentId, { isPresent: e.target.checked })
                  }
                />
              </td>
              <td>
                <input
                  type="number"
                  min="1"
                  max="5"
                  value={s.grade ?? ""}
                  disabled={!s.isPresent}
                  onChange={e =>
                    update(s.studentId, {
                      grade: e.target.value
                        ? Number(e.target.value)
                        : null
                    })
                  }
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <button className="save-btn" onClick={saveAll}>
        Save all
      </button>
    </>
  );
}
