import { useEffect, useState } from "react";
import { getMyCourses } from "../../api/studentCoursesService";
import "./StudentCourses.css";

export default function StudentCourses() {
  const [courses, setCourses] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function loadCourses() {
      try {
        const data = await getMyCourses();
        setCourses(data);
      } catch (err) {
        console.error("Courses load error:", err);
      } finally {
        setLoading(false);
      }
    }

    loadCourses();
  }, []);

  if (loading) {
    return <div className="student-loading">Loading courses...</div>;
  }

  return (
    <div className="student-courses">
      <header className="courses-header">
        <h1>My Courses</h1>
        <p>All courses you are currently enrolled in</p>
      </header>

      {courses.length === 0 ? (
        <div className="empty-state">
          <p>You are not enrolled in any courses yet.</p>
        </div>
      ) : (
        <div className="courses-grid">
          {courses.map(course => (
            <div className="course-card" key={course.id}>
              <div className="course-card-header">
                <h3>{course.title}</h3>
                <span className="badge">{course.semester}</span>
              </div>

              <p className="course-description">
                {course.description || "No description provided."}
              </p>

              <div className="course-meta">
                <span>
                  👨‍🏫 {course.teacherName}
                </span>
                <span>
                  📚 {course.credits} credits
                </span>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
