import { useEffect, useState } from "react";
import {
  getTeacherTasks,
  createTask,
  deleteTask,
  getTaskSubmissions
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
    courseId: ""
  });

  useEffect(() => {
    loadData();
  }, []);

  async function loadData() {
    setLoading(true);
    const [tasksData, coursesData] = await Promise.all([
      getTeacherTasks(),
      getMyCourses()
    ]);

    setTasks(tasksData);
    setCourses(coursesData);
    setLoading(false);
  }

  async function handleCreate(e) {
    e.preventDefault();

    await createTask({
      title: form.title,
      description: form.description,
      deadline: form.deadline,
      courseId: Number(form.courseId)
    });

    setForm({
      title: "",
      description: "",
      deadline: "",
      courseId: ""
    });

    loadData();
  }

  async function openTask(taskId) {
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
      <h1>My Tasks</h1>

      {/* ===== CREATE TASK ===== */}
      <form className="task-form" onSubmit={handleCreate}>
        <h2>Create task</h2>

        <input
          placeholder="Title"
          value={form.title}
          onChange={(e) => setForm({ ...form, title: e.target.value })}
          required
        />

        <textarea
          placeholder="Description"
          value={form.description}
          onChange={(e) => setForm({ ...form, description: e.target.value })}
        />

        <input
          type="datetime-local"
          value={form.deadline}
          onChange={(e) => setForm({ ...form, deadline: e.target.value })}
          required
        />

        <select
          value={form.courseId}
          onChange={(e) => setForm({ ...form, courseId: e.target.value })}
          required
        >
          <option value="">Select course</option>
          {courses.map((c) => (
            <option key={c.id} value={c.id}>
              {c.title}
            </option>
          ))}
        </select>

        <button>Create</button>
      </form>

      {/* ===== TASK LIST ===== */}
      <div className="task-list">
        {tasks.map((task) => (
          <div className="task-card" key={task.id}>
            <div className="task-header" onClick={() => openTask(task.id)}>
              <div>
                <h3>{task.title}</h3>
                <small>
                  Deadline: {new Date(task.deadline).toLocaleString()}
                </small>
              </div>

              <div className="actions">
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
            </div>

            {openedTaskId === task.id && (
              <div className="submissions">
                <h4>Students</h4>

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
                          <td>
                            <span className={`status ${s.status.toLowerCase()}`}>
                              {s.status}
                            </span>
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
            )}
          </div>
        ))}
      </div>
    </div>
  );
}
