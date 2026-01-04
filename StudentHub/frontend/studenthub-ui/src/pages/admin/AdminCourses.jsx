import { useEffect, useState } from "react";
import api from "../../api/axios";
import "./AdminCourses.css";

export default function AdminCourses() {
  const [courses, setCourses] = useState([]);
  const [teachers, setTeachers] = useState([]);

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [teacherId, setTeacherId] = useState("");

  const loadData = async () => {
    try {
      const coursesRes = await api.get("/courses");
      const usersRes = await api.get("/admin/users");

      const teacherUsers = usersRes.data.filter(u =>
        u.roles.includes("Teacher")
      );

      setCourses(coursesRes.data);
      setTeachers(teacherUsers);
    } catch (e) {
      alert("Error loading data");
    }
  };

  useEffect(() => {
    loadData();
  }, []);

  const createCourse = async () => {
    if (!title || !teacherId) {
      alert("Title and teacher are required");
      return;
    }

    await api.post("/courses", {
      title,
      description,
      teacherId
    });

    setTitle("");
    setDescription("");
    setTeacherId("");

    loadData();
  };

  const deleteCourse = async (id) => {
    if (!window.confirm("Delete this course?")) return;
    await api.delete(`/courses/${id}`);
    loadData();
  };

  return (
    <div className="courses-page">
      <div className="courses-header">
        <h1>Admin → Courses</h1>
      </div>

      <div className="course-form">
        <h3>Create course</h3>

        <input
          placeholder="Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />

        <textarea
          placeholder="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />

        <select
          value={teacherId}
          onChange={(e) => setTeacherId(e.target.value)}
        >
          <option value="">Select teacher</option>
          {teachers.map(t => (
            <option key={t.id} value={t.id}>
              {t.firstName} {t.lastName} ({t.email})
            </option>
          ))}
        </select>

        <button onClick={createCourse}>Create course</button>
      </div>

      <div className="courses-list">
        <table>
          <thead>
            <tr>
              <th>Title</th>
              <th>Description</th>
              <th>Teacher</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {courses.map(c => {
              const teacher = teachers.find(t => t.id === c.teacherId);

              return (
                <tr key={c.id}>
                  <td>{c.title}</td>
                  <td>{c.description}</td>
                  <td>
                    {teacher
                      ? `${teacher.firstName} ${teacher.lastName}`
                      : "—"}
                  </td>
                  <td className="course-actions">
                    <button
                      className="delete"
                      onClick={() => deleteCourse(c.id)}
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
}
