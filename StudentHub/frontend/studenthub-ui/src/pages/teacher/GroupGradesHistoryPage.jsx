import { useEffect, useState } from "react";
import { getGradesHistory } from "../../api/teacherGroupService";
export default function GroupGradesHistoryPage({ groupId, courseId }) {
  const [rows, setRows] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadHistory();
  }, [groupId, courseId]);

  async function loadHistory() {
    try {
      const data = await getGradesHistory(groupId, courseId);
      setRows(data);
    } catch (e) {
      console.error("Failed to load history", e);
    } finally {
      setLoading(false);
    }
  }

  if (loading) return <p>Loading history...</p>;

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
            <td>{r.date}</td>
            <td>{r.isPresent ? "✔" : "—"}</td>
            <td>{r.grade ?? "—"}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
