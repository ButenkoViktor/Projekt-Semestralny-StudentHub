import { useEffect, useState } from "react";
import {
  getCurrentUser,
  getTodaySchedule,
  getUpcomingDeadlines,
  getAnnouncements,
  getStudyStats
} from "../../api/studentService";
import "./StudentHome.css";

export default function StudentHome() {
  const [user, setUser] = useState(null);
  const [schedule, setSchedule] = useState([]);
  const [deadlines, setDeadlines] = useState([]);
  const [announcements, setAnnouncements] = useState([]);
  const [stats, setStats] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function loadDashboard() {
      try {
        const [
          userData,
          scheduleData,
          deadlineData,
          announcementData,
          statsData
        ] = await Promise.all([
          getCurrentUser(),
          getTodaySchedule(),
          getUpcomingDeadlines(),
          getAnnouncements(),
          getStudyStats()
        ]);

        setUser(userData);
        setSchedule(scheduleData);
        setDeadlines(deadlineData);
        setAnnouncements(announcementData);
        setStats(statsData);
      } catch (e) {
        console.error("StudentHome error:", e);
      } finally {
        setLoading(false);
      }
    }

    loadDashboard();
  }, []);

  if (loading) {
    return <div className="student-loading">Loading dashboard...</div>;
  }

  return (
    <div className="student-home">
      <section className="welcome-card">
          {user && (
            <h1>
                Welcome back, <span>{user.firstName}</span>
            </h1>
          )}
        <p>Your academic overview for today</p>
      </section>

      <div className="dashboard-grid">
 
        <section className="dashboard-card">
          <h2>Schedule Today</h2>

          {schedule.length === 0 ? (
            <p className="empty">No classes today 🎉</p>
          ) : (
            <ul className="schedule-list">
              {schedule.map(item => (
                <li key={item.id}>
                  <div>
                    <strong>{item.courseTitle}</strong>
                    <span>{item.lessonType}</span>
                  </div>
                  <div className="time">
                    {new Date(item.startTime).toLocaleTimeString([], {
                      hour: "2-digit",
                      minute: "2-digit"
                    })}
                    {" - "}
                    {new Date(item.endTime).toLocaleTimeString([], {
                      hour: "2-digit",
                      minute: "2-digit"
                    })}
                  </div>
                </li>
              ))}
            </ul>
          )}
        </section>

        <section className="dashboard-card highlight">
          <h2>Upcoming Deadlines</h2>

          {deadlines.length === 0 ? (
            <p className="empty">No upcoming deadlines</p>
          ) : (
            <ul className="deadline-list">
              {deadlines.map(task => (
                <li key={task.id}>
                  <div>
                    <strong>{task.title}</strong>
                    <span>{task.description}</span>
                  </div>
                  <div className="deadline-date">
                    {new Date(task.deadline).toLocaleDateString()}
                  </div>
                </li>
              ))}
            </ul>
          )}
        </section>

        <section className="dashboard-card">
          <h2>Announcements</h2>

          {announcements.length === 0 ? (
            <p className="empty">No announcements</p>
          ) : (
            <ul className="announcement-list">
              {announcements.slice(0, 5).map(a => (
                <li key={a.id}>
                  <strong>{a.title}</strong>
                  <p>{a.content}</p>
                </li>
              ))}
            </ul>
          )}
        </section>

        <section className="dashboard-card stats">
          <h2>Study Status</h2>

          {stats && (
            <div className="stats-grid">
              <div>
                <span>{stats.totalTasks}</span>
                <label>Total Tasks</label>
              </div>
              <div>
                <span>{stats.pendingTasks}</span>
                <label>Pending Tasks</label>
              </div>
            </div>
          )}
        </section>
      </div>
    </div>
  );
}