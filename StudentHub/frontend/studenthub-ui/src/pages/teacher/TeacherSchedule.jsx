import { useEffect, useState } from "react";
import { getSchedule, createSchedule, deleteSchedule } from "../../api/scheduleService";
import "./TeacherSchedule.css";

const TEACHER_NAME = "Viktor Butenko"; // Hardcoded for demo purposes
export default function TeacherSchedule() {
  const [items, setItems] = useState([]);
  const [view, setView] = useState("week");

  const [form, setForm] = useState({
    courseId: "",
    startTime: "",
    endTime: "",
    lessonType: "",
    groupId: "",
  });

  useEffect(() => {
    load();
  }, []);

  async function load() {
    const data = await getSchedule();
    setItems(data.filter(i => i.teacherName === TEACHER_NAME));
  }

  async function handleCreate(e) {
    e.preventDefault();

    await createSchedule({
      courseId: Number(form.courseId),
      teacherName: TEACHER_NAME,
      startTime: form.startTime,
      endTime: form.endTime,
      lessonType: form.lessonType || null,
      groupId: form.groupId ? Number(form.groupId) : null,
    });

    setForm({
      courseId: "",
      startTime: "",
      endTime: "",
      lessonType: "",
      groupId: "",
    });

    load();
  }

  const filtered = items.filter(i => {
    const d = new Date(i.startTime);
    const now = new Date();

    if (view === "day") {
      return d.toDateString() === now.toDateString();
    }

    const weekStart = new Date(now);
    weekStart.setDate(now.getDate() - now.getDay());

    const weekEnd = new Date(weekStart);
    weekEnd.setDate(weekStart.getDate() + 7);

    return d >= weekStart && d <= weekEnd;
  });

  return (
    <div className="schedule-page">
      <div className="schedule-header">
        <h1>Teacher Schedule</h1>
        <div className="view-switch">
          <button onClick={() => setView("day")} className={view === "day" ? "active" : ""}>Day</button>
          <button onClick={() => setView("week")} className={view === "week" ? "active" : ""}>Week</button>
        </div>
      </div>

      <form className="schedule-form" onSubmit={handleCreate}>
        <h2>Add lesson</h2>

        <input
          placeholder="Course ID"
          value={form.courseId}
          onChange={e => setForm({ ...form, courseId: e.target.value })}
          required
        />

        <input
          type="datetime-local"
          value={form.startTime}
          onChange={e => setForm({ ...form, startTime: e.target.value })}
          required
        />

        <input
          type="datetime-local"
          value={form.endTime}
          onChange={e => setForm({ ...form, endTime: e.target.value })}
          required
        />

        <input
          placeholder="Lesson type (lecture, lab)"
          value={form.lessonType}
          onChange={e => setForm({ ...form, lessonType: e.target.value })}
        />

        <input
          placeholder="Group ID (optional)"
          value={form.groupId}
          onChange={e => setForm({ ...form, groupId: e.target.value })}
        />

        <button>Create</button>
      </form>

      <div className="schedule-grid">
        {filtered.map(item => (
          <div key={item.id} className="schedule-card">
            <div>
              <h3>{item.course.title}</h3>
              <p>{item.lessonType || "Lesson"}</p>
              <small>
                {new Date(item.startTime).toLocaleString()} –{" "}
                {new Date(item.endTime).toLocaleTimeString()}
              </small>
            </div>

            <button
              className="delete"
              onClick={() => deleteSchedule(item.id).then(load)}
            >
              Delete
            </button>
          </div>
        ))}

        {filtered.length === 0 && (
          <p className="empty">No lessons</p>
        )}
      </div>
    </div>
  );
}
