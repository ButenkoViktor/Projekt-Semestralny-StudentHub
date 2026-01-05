import { useEffect, useState } from "react";
import { getMyCourses } from "../../api/coursesService";
import "./StudentCourses.css";

export default function StudentCourses() {
  const [courses, setCourses] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getMyCourses()
      .then(setCourses)
      .finally(() => setLoading(false));
  }, []);

  if (loading) {
    return <div className="student-loading">Loading courses...</div>;
  }

  return (
    <div className="student-home">
      <section className="welcome-card">
        <h1>My Courses</h1>
        <p>Courses assigned to your group</p>
      </section>

      <div className="dashboard-grid">
        {courses.map(course => (
          <section className="dashboard-card" key={course.id}>
            <h2>{course.title}</h2>
            <p className="course-desc">{course.description}</p>

            <div className="teacher-info">
              <strong>Teacher</strong>
              <span>{course.teacherFirstName} {course.teacherLastName}</span>
              <small>{course.teacherEmail}</small>
            </div>
          </section>
        ))}

        {courses.length === 0 && (
          <section className="dashboard-card">
            <p className="empty">No courses assigned</p>
          </section>
        )}
      </div>
    </div>
  );
}
