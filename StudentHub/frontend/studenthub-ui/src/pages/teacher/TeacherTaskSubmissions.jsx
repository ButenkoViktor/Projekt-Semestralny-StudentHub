import { useEffect, useState } from "react";
import { getTaskSubmissions } from "../../api/tasksService";
import "./TeacherTaskSubmissions.css";

export default function TeacherTaskSubmissions({ task }) {
  const [submissions, setSubmissions] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function load() {
      setLoading(true);
      const data = await getTaskSubmissions(task.id);
      setSubmissions(data);
      setLoading(false);
    }
    load();
  }, [task.id]);

  if (loading) return <p>Loading submissions...</p>;

  return (
    <div className="submissions">
      <h4>Submissions</h4>

      {submissions.length === 0 ? (
        <p>No submissions yet</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>Student</th>
              <th>Status</th>
              <th>Submitted at</th>
            </tr>
          </thead>
          <tbody>
            {submissions.map((s) => (
              <tr key={s.id}>
                <td>{s.user.firstName} {s.user.lastName}</td>
                <td className={`status ${s.status.toLowerCase()}`}>
                  {s.status}
                </td>
                <td>
                  {s.submittedAt
                    ? new Date(s.submittedAt).toLocaleString()
                    : "—"}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}