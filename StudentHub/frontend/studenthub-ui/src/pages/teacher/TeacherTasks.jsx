import { useEffect, useState } from "react";
import {
  getTeacherTasks,
  createTask,
  deleteTask,
  getTaskSubmissions,
} from "../../api/tasksService";
import { getMyCourses } from "../../api/teacherCoursesService";
import "./TeacherTasks.css";

export default function TeacherTasks() {
  const [tasks, setTasks] = useState([]);
  const [courses, setCourses] = useState([]);
  const [openedTaskId, setOpenedTaskId] = useState(null);
  const [submissions, setSubmissions] = useState({});
  const [loading, setLoading] = useState(true);

  const [form, setForm] = useState({
    title: "",
    description: "",
    deadline: "",
    courseId: "",
    groupId: "",
  });

  useEffect(() => {
    loadData();
  }, []);

  async function loadData() {
    setLoading(true);
    const [tasksData, coursesData] = await Promise.all([
      getTeacherTasks(),
      getMyCourses(),
    ]);
    setTasks(tasksData);
    setCourses(coursesData);
    setLoading(false);
  }

  async function handleCreate(e) {
    e.preventDefault();

    await createTask({
      title: form.title,
      description: form.description || null,
      deadline: form.deadline,
      courseId: Number(form.courseId),
      groupId: form.groupId ? Number(form.groupId) : null,
    });

    setForm({
      title: "",
      description: "",
      deadline: "",
      courseId: "",
      groupId: "",
    });

    loadData();
  }

  async function toggleTask(taskId) {
    if (openedTaskId === taskId) {
      setOpenedTaskId(null);
      return;
    }

    setOpenedTaskId(taskId);

    if (!submissions[taskId]) {
      const data = await getTaskSubmissions(taskId);
      setSubmissions((prev) => ({ ...prev, [taskId]: data }));
    }
  }

  if (loading) return <div className="tasks-page">Loading...</div>;

  return (
    <div className="tasks-page">

      <div className="welcome-card">
        <h1>Teacher Tasks</h1>
        <p>Create assignments, manage deadlines and review submissions</p>
      </div>

      <section className="create-task-section">
        <form className="task-form" onSubmit={handleCreate}>
          <h2>Create task</h2>

          <input
            placeholder="Task title"
            value={form.title}
            onChange={(e) => setForm({ ...form, title: e.target.value })}
            required
          />

          <textarea
            placeholder="Task description"
            value={form.description}
            onChange={(e) =>
              setForm({ ...form, description: e.target.value })
            }
          />

          <input
            type="datetime-local"
            value={form.deadline}
            onChange={(e) =>
              setForm({ ...form, deadline: e.target.value })
            }
            required
          />

          <select
            value={form.courseId}
            onChange={(e) =>
              setForm({ ...form, courseId: e.target.value })
            }
            required
          >
            <option value="">Select course</option>
            {courses.map((c) => (
              <option key={c.id} value={c.id}>
                {c.title}
              </option>
            ))}
          </select>

          <input
            placeholder="Group ID (optional)"
            value={form.groupId}
            onChange={(e) =>
              setForm({ ...form, groupId: e.target.value })
            }
          />

          <button type="submit">Create task</button>
        </form>
      </section>

      <section className="tasks-section">
        <div className="task-list">
          {tasks.map((task) => (
            <div key={task.id} className="task-card">
              <div
                className="task-card-header"
                onClick={() => toggleTask(task.id)}
              >
                <div>
                  <h3>{task.title}</h3>
                  <small>
                    Deadline:{" "}
                    {new Date(task.deadline).toLocaleString()}
                  </small>
                </div>

                <button
                  className="delete"
                  onClick={(e) => {
                    e.stopPropagation();
                    deleteTask(task.id).then(loadData);
                  }}
                >
                  Delete
                </button>
              </div>

              {openedTaskId === task.id && (
                <div className="submissions">
                  <h4>Submissions</h4>

                  {!submissions[task.id] ||
                  submissions[task.id].length === 0 ? (
                    <p className="empty">No submissions</p>
                  ) : (
                    <table>
                      <thead>
                        <tr>
                          <th>Student</th>
                          <th>Status</th>
                          <th>Submitted</th>
                        </tr>
                      </thead>
                      <tbody>
                        {submissions[task.id].map((s) => (
                          <tr key={s.id}>
                            <td>
                              {s.user.firstName} {s.user.lastName}
                            </td>
                            <td>{s.status}</td>
                            <td>
                              {s.submittedAt
                                ? new Date(
                                    s.submittedAt
                                  ).toLocaleString()
                                : "—"}
                            </td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  )}
                </div>
              )}
            </div>
          ))}
        </div>
      </section>
    </div>
  );
}