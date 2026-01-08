import { useEffect, useState } from "react";
import { getStudentTasks, submitTask } from "../../api/tasksService";
import "./StudentTasks.css";

export default function StudentTasks() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [answers, setAnswers] = useState({});

  useEffect(() => {
    loadTasks();
  }, []);

  const loadTasks = async () => {
    try {
      const data = await getStudentTasks();
      setTasks(data);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (taskId) => {
    if (!answers[taskId]) {
      alert("Enter your answer");
      return;
    }

    await submitTask(taskId, {
      answerText: answers[taskId]
    });

    setAnswers(prev => ({ ...prev, [taskId]: "" }));
    loadTasks();
  };

  const getStatus = (task) => {
    const submission = task.submissions?.[0];

    if (!submission) {
      return { text: "Pending", className: "pending" };
    }

    switch (submission.status) {
      case 0:
      case "Pending":
        return { text: "Pending", className: "pending" };

      case 1:
      case "Submitted":
        return { text: "Submitted", className: "submitted" };

      case 2:
      case "Graded":
        return { text: "Graded", className: "graded" };

      default:
        return { text: "Unknown", className: "pending" };
    }
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
                <span className={`task-status ${status.className}`}>
                  {status.text}
                </span>
                <small>{getCountdown(task.deadline)}</small>
              </div>

              {status.className === "pending" && (
                <div className="task-submit">
                  <textarea
                    placeholder="Your answer..."
                    value={answers[task.id] || ""}
                    onChange={(e) =>
                      setAnswers(prev => ({
                        ...prev,
                        [task.id]: e.target.value
                      }))
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
