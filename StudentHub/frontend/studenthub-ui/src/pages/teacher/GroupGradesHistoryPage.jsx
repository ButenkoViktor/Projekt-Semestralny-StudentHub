import { useEffect, useState } from "react";
import { getGradesHistory } from "../../api/teacherGroupService";

export default function GroupGradesHistoryPage({ groupId, courseId }) {
  const [rows, setRows] = useState([]);

  useEffect(() => {
    getGradesHistory(groupId, courseId).then(setRows);
  }, [groupId, courseId]);

  return (
    <table className="students-table">
      <thead>
        <tr>
          <th>Student</th>
          <th>Date</th>
          <th>Present</th>
          <th>Grade</th>
        </tr>
      </thead>
      <tbody>
        {rows.map((r, i) => (
          <tr key={i}>
            <td>{r.studentName}</td>
            <td>{r.date.slice(0,10)}</td>
            <td>{r.isPresent ? "✔" : "—"}</td>
            <td>{r.grade ?? "—"}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}