import { useEffect, useState } from "react";
import { saveGrade } from "../../api/teacherGroupService";

export default function GroupStudentsTable({
  students,
  groupId,
  courseId,
  lessonDate
}) {
  const [rows, setRows] = useState([]);

  useEffect(() => {
    setRows(students);
  }, [students]);

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
      date: lessonDate,
      grade: row.grade,
      isPresent: row.isPresent
    });
    alert("Saved ✔");
  };

  const avg = (r) =>
    r.filter(x => x.grade != null)
     .reduce((s, x) => s + x.grade, 0) /
    (r.filter(x => x.grade != null).length || 1);

  return (
    <>
      <table className="students-table">
        <thead>
          <tr>
            <th>Student</th>
            <th>Present</th>
            <th>Grade</th>
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
                    <option key={v}>{v}</option>
                  ))}
                </select>
              </td>
              <td>
                <button onClick={() => submit(s)}>Save</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <div className="final-grade">
        Group average: <strong>{avg(rows).toFixed(2)}</strong>
      </div>
    </>
  );
}