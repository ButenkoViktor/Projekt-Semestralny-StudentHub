import { useEffect, useState } from "react";
import {
  getTeacherProfile,
  getTeacherCourses,
  getTeacherTasks,
  getTeacherSchedule
} from "../../api/teacherService";
import "./TeacherHome.css";

export default function TeacherHome() {
  const [teacher, setTeacher] = useState(null);
  const [courses, setCourses] = useState([]);
  const [tasks, setTasks] = useState([]);
  const [schedule, setSchedule] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function load() {
      try {
        const [
          teacherData,
          coursesData,
          tasksData,
          scheduleData
        ] = await Promise.all([
          getTeacherProfile(),
          getTeacherCourses(),
          getTeacherTasks(),
          getTeacherSchedule()
        ]);

        setTeacher(teacherData);
        setCourses(coursesData);
        setTasks(tasksData);
        setSchedule(scheduleData);
      } catch (e) {
        console.error("TeacherHome error:", e);
      } finally {
        setLoading(false);
      }
    }

    load();
  }, []);

  if (loading) {
    return <div className="teacher-loading">Loading dashboard...</div>;
  }

  return (
    <div className="teacher-home">
      <section className="welcome-card">
        <h1>
          Welcome, <span>{teacher.firstName}</span>
        </h1>
        <p>Your teaching overview</p>
      </section>

      <div className="dashboard-grid">
        <DashboardCard
          title="My Courses"
          emptyText="No assigned courses"
          items={courses}
          renderItem={c => (
            <>
              <strong>{c.title}</strong>
              <span>{c.groupName}</span>
            </>
          )}
        />

        <DashboardCard
          title="Active Tasks"
          highlight
          emptyText="No active tasks"
          items={tasks.slice(0, 5)}
          renderItem={t => (
            <>
              <strong>{t.title}</strong>
              <span>{new Date(t.deadline).toLocaleDateString()}</span>
            </>
          )}
        />

        <DashboardCard
          title="Upcoming Lessons"
          emptyText="No lessons planned"
          items={schedule.slice(0, 5)}
          renderItem={s => (
            <>
              <strong>{s.courseTitle}</strong>
              <span>
                {new Date(s.startTime).toLocaleTimeString([], {
                  hour: "2-digit",
                  minute: "2-digit"
                })}
              </span>
            </>
          )}
        />
      </div>
    </div>
  );
}

function DashboardCard({ title, items, emptyText, renderItem, highlight }) {
  return (
    <section className={`dashboard-card ${highlight ? "highlight" : ""}`}>
      <h2>{title}</h2>

      {items.length === 0 ? (
        <p className="empty">{emptyText}</p>
      ) : (
        <ul className="simple-list">
          {items.map((item, i) => (
            <li key={item.id || i}>{renderItem(item)}</li>
          ))}
        </ul>
      )}
    </section>
  );
}