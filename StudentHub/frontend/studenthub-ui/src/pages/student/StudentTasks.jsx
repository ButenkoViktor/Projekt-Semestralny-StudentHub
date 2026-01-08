import { useEffect, useState } from "react";
import { getStudentTasks, submitTask, updateSubmission } from "../../api/tasksService";
import "./StudentTasks.css";

export default function StudentTasks() {
  const [tasks, setTasks] = useState([]);
  const [answers, setAnswers] = useState({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadTasks();
  }, []);

  async function loadTasks() {
    setLoading(true);
    const data = await getStudentTasks();
    setTasks(data);

    const initial = {};
    data.forEach(t => {
      if (t.submissions?.[0]) {
        initial[t.id] = t.submissions[0].answerText || "";
      }
    });
    setAnswers(initial);

    setLoading(false);
  }

  const handleSave = async (task) => {
    const text = answers[task.id];
    if (!text) return alert("Answer required");

    const hasSubmission = task.submissions?.length > 0;

    try {
      if (hasSubmission) {
        await updateSubmission(task.id, { answerText: text });
      } else {
        await submitTask(task.id, { answerText: text });
      }
      loadTasks();
    } catch (e) {
      alert(e.response?.data || "Error");
    }
  };

  const deadlinePassed = (deadline) =>
    new Date(deadline) < new Date();

  if (loading) return <div className="student-page">Loading...</div>;

  return (
    <div className="student-page">
      {/* 🔵 синя плашка */}
      <div className="student-hero">
        <h1>My tasks</h1>
        <p>Assignments you need to complete</p>
      </div>

      {/* 📦 cards */}
      <div className="tasks-grid">
        {tasks.map(task => {
          const sub = task.submissions?.[0];
          const locked = deadlinePassed(task.deadline);

          return (
            <div className="task-card" key={task.id}>
              <h2>{task.title}</h2>
              <p className="desc">{task.description}</p>

              <div className="meta">
                <span
                  className={`status ${
                    !sub ? "pending" : sub.status === 1 ? "late" : "submitted"
                  }`}
                >
                  {!sub ? "Pending" : sub.status === 1 ? "Late" : "Submitted"}
                </span>
                <small>{new Date(task.deadline).toLocaleString()}</small>
              </div>

              <div className="submit-box">
                <textarea
                  disabled={locked}
                  value={answers[task.id] || ""}
                  onChange={e =>
                    setAnswers({ ...answers, [task.id]: e.target.value })
                  }
                />

                {!locked ? (
                  <button onClick={() => handleSave(task)}>
                    {sub ? "Update answer" : "Submit"}
                  </button>
                ) : (
                  <p className="locked">Deadline passed</p>
                )}
              </div>
            </div>
          );
        })}

        {tasks.length === 0 && (
          <p className="empty">No tasks assigned</p>
        )}
      </div>
    </div>
  );
}
