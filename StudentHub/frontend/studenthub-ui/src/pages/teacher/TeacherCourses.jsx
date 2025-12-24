import { useEffect, useState } from "react";
import { getAllCourses } from "../../api/teacherCoursesService";
import "./TeacherCourses.css";

export default function TeacherCourses() {
  const [courses, setCourses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function load() {
      try {
        const data = await getAllCourses();
        setCourses(data);
      } catch (e) {
        setError(e.message);
      } finally {
        setLoading(false);
      }
    }

    load();
  }, []);

  if (loading) {
    return <div className="teacher-loading">Loading courses...</div>;
  }

  if (error) {
    return <div className="teacher-error">{error}</div>;
  }

  return (
    <div className="teacher-page">
      <section className="welcome-card">
        <h1>Courses</h1>
        <p>All available courses in the system</p>
      </section>

      <div className="dashboard-grid">
        {courses.length === 0 ? (
          <p className="empty">No courses available</p>
        ) : (
          courses.map(course => (
            <div className="dashboard-card course-card" key={course.id}>
              <h2>{course.title}</h2>

              {course.description && (
                <p className="course-desc">{course.description}</p>
              )}
            </div>
          ))
        )}
      </div>
    </div>
  );
}