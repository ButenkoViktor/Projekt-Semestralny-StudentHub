import { saveGrade } from "../../api/teacherGroupService";

export default function GroupStudentsTable({students, setStudents, groupId, courseId, date}) 
{
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
        date,
        grade: s.grade,
        isPresent: s.isPresent
      });
    }
    alert("Saved successfully ✅");
  };

  return (
    <>
      <table className="table">
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
                    update(s.studentId, {
                      isPresent: e.target.checked
                    })
                  }
                />
              </td>
              <td>
                <input
                  type="number"
                  min="1"
                  max="5"
                  disabled={!s.isPresent}
                  value={s.grade ?? ""}
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
        Save lesson
      </button>
    </>
  );
}
