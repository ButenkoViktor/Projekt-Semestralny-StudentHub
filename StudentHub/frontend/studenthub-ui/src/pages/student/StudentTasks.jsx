import { useEffect, useState } from "react";
import { getStudentTasks, submitTask } from "../../api/tasksService";
import "./StudentTasks.css";

export default function StudentTasks() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [answers, setAnswers] = useState({});

  useEffect(() => {
    getStudentTasks()
      .then(setTasks)
      .finally(() => setLoading(false));
  }, []);

  const handleSubmit = async (taskId) => {
    if (!answers[taskId]) return alert("Enter your answer");

    await submitTask(taskId, {
      answerText: answers[taskId]
    });

    // перезавантажуємо задачі щоб оновився статус
    const updated = await getStudentTasks();
    setTasks(updated);
    setAnswers(prev => ({ ...prev, [taskId]: "" }));
  };

  const getStatus = (task) => {
    if (!task.submissions || task.submissions.length === 0)
      return "pending";

    return task.submissions[0].status.toLowerCase();
  };

  const getCountdown = (deadline) => {
    const diff = new Date(deadline) - new Date();
    if (diff <= 0) return "Deadline passed";

    const hours = Math.floor(diff / 1000 / 60 / 60);
    const minutes = Math.floor((diff / 1000 / 60) % 60);

    return `${hours}h ${minutes}m left`;
  };

  if (loading) {
    return <div className="student-loading">Loading tasks...</div>;
  }

  return (
    <div className="student-home">
      <section className="welcome-card">
        <h1>My Tasks</h1>
        <p>Tasks assigned to your courses</p>
      </section>

      <div className="dashboard-grid">
        {tasks.map(task => {
          const status = getStatus(task);

          return (
            <section className="dashboard-card" key={task.id}>
              <h2>{task.title}</h2>
              <p className="task-desc">{task.description}</p>

              <div className="task-meta">
                <span className={`task-status ${status}`}>
                  {status}
                </span>
                <small>{getCountdown(task.deadline)}</small>
              </div>

              {status === "pending" && (
                <div className="task-submit">
                  <textarea
                    placeholder="Your answer..."
                    value={answers[task.id] || ""}
                    onChange={(e) =>
                      setAnswers({ ...answers, [task.id]: e.target.value })
                    }
                  />
                  <button onClick={() => handleSubmit(task.id)}>
                    Submit
                  </button>
                </div>
              )}
            </section>
          );
        })}

        {tasks.length === 0 && (
          <section className="dashboard-card">
            <p className="empty">No tasks assigned</p>
          </section>
        )}
      </div>
    </div>
  );
}
