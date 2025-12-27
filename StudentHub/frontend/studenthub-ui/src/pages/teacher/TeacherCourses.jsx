import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getTeacherCourses } from "../../api/teacherService";
import "./TeacherCourses.css";

export default function TeacherCourses() {
  const [courses, setCourses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    async function load() {
      try {
        const data = await getTeacherCourses();
        setCourses(data);
      } catch {
        setError("Failed to load courses");
      } finally {
        setLoading(false);
      }
    }

    load();
  }, []);

  if (loading) return <div className="teacher-loading">Loading courses...</div>;
  if (error) return <div className="teacher-error">{error}</div>;

  return (
    <div className="teacher-page">
      <section className="welcome-card">
        <h1>My Courses</h1>
        <p>Courses assigned to you by administrator</p>
      </section>

      <div className="courses-grid">
        {courses.length === 0 ? (
          <p className="empty">You have no assigned courses yet</p>
        ) : (
          courses.map(course => (
            <div className="course-card" key={course.id}>
              <div className="course-header">
                <h2>{course.title}</h2>
                {course.studyYear && (
                  <span className="course-year">
                    {course.studyYear} year
                  </span>
                )}
              </div>

              {course.description && (
                <p className="course-desc">{course.description}</p>
              )}

              {course.groups?.length > 0 && (
                <div className="course-groups">
                  {course.groups.map(g => (
                    <span key={g.id} className="group-pill">
                      {g.name}
                    </span>
                  ))}
                </div>
              )}

              <div className="course-actions">
                <button
                  onClick={() =>
                    navigate(`/teacher/tasks?courseId=${course.id}`)
                  }
                >
                  Tasks
                </button>
                <button
                  onClick={() =>
                    navigate(`/teacher/schedule?courseId=${course.id}`)
                  }
                >
                  Schedule
                </button>
              </div>
            </div>
          ))
        )}
      </div>
    </div>
  );
}