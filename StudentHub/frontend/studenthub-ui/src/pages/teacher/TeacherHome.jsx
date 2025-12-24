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

        <section className="dashboard-card">
          <h2>My Courses</h2>
          {courses.length === 0 ? (
            <p className="empty">No assigned courses</p>
          ) : (
            <ul className="simple-list">
              {courses.map(c => (
                <li key={c.id}>
                  <strong>{c.title}</strong>
                  <span>{c.groupName}</span>
                </li>
              ))}
            </ul>
          )}
        </section>

        <section className="dashboard-card highlight">
          <h2>Active Tasks</h2>
          {tasks.length === 0 ? (
            <p className="empty">No active tasks</p>
          ) : (
            <ul className="simple-list">
              {tasks.slice(0, 5).map(t => (
                <li key={t.id}>
                  <strong>{t.title}</strong>
                  <span>
                    {new Date(t.deadline).toLocaleDateString()}
                  </span>
                </li>
              ))}
            </ul>
          )}
        </section>

        <section className="dashboard-card">
          <h2>Upcoming Lessons</h2>
          {schedule.length === 0 ? (
            <p className="empty">No lessons planned</p>
          ) : (
            <ul className="simple-list">
              {schedule.slice(0, 5).map(s => (
                <li key={s.id}>
                  <strong>{s.courseTitle}</strong>
                  <span>
                    {new Date(s.startTime).toLocaleTimeString([], {
                      hour: "2-digit",
                      minute: "2-digit"
                    })}
                  </span>
                </li>
              ))}
            </ul>
          )}
        </section>

      </div>
    </div>
  );
}