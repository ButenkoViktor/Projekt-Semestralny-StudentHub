import { useState, useEffect } from "react";
import { saveGrade } from "../../api/teacherGroupService";

export default function GroupStudentsTable({
  students,
  groupId,
  courseId
}) {
    const [rows, setRows] = useState([]);
    useEffect(() => {setRows(students);}, [students]);

  const update = (i, field, value) => {
    const copy = [...rows];
    copy[i][field] = value;
    setRows(copy);
  };

  const submit = async (row) => {
    await saveGrade({
      studentId: row.studentId,
      groupId,
      courseId,
      date: new Date().toISOString(),
      grade: row.grade,
      isPresent: row.isPresent
    });
  };

  const finalAvg =
    rows.reduce((s, r) => s + (r.grade ?? 0), 0) /
    rows.filter(r => r.grade !== null).length || 0;

  return (
    <>
      <table className="students-table">
        <thead>
          <tr>
            <th>Student</th>
            <th>Present</th>
            <th>Grade (1–5)</th>
            <th>Save</th>
          </tr>
        </thead>
        <tbody>
          {rows.map((s, i) => (
            <tr key={s.studentId}>
              <td>{s.studentName}</td>

              <td>
                <input
                  type="checkbox"
                  checked={s.isPresent}
                  onChange={e =>
                    update(i, "isPresent", e.target.checked)
                  }
                />
              </td>

              <td>
                <select
                  value={s.grade ?? ""}
                  onChange={e =>
                    update(i, "grade", Number(e.target.value))
                  }
                >
                  <option value="">–</option>
                  {[1,2,3,4,5].map(v => (
                    <option key={v} value={v}>{v}</option>
                  ))}
                </select>
              </td>

              <td>
                <button onClick={() => submit(s)}>
                  Save
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <div className="final-grade">
        Final semester average: <strong>{finalAvg.toFixed(2)}</strong>
      </div>
    </>
  );
}