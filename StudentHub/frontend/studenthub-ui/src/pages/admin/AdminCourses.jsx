import { useEffect, useState } from "react";
import {
  getAllCourses,
  createCourse,
  updateCourse,
  deleteCourse
} from "../../api/adminCourseService";
import axios from "../../api/axios";
import "./AdminCourses.css";

export default function AdminCourses() {
  const [courses, setCourses] = useState([]);
  const [teachers, setTeachers] = useState([]);
  const [loading, setLoading] = useState(true);

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [teacherId, setTeacherId] = useState("");
  const [editingId, setEditingId] = useState(null);

  useEffect(() => {
    load();
    loadTeachers();
  }, []);

  async function load() {
    try {
      setLoading(true);
      const data = await getAllCourses();
      setCourses(data);
    } catch (e) {
      alert(e.message);
    } finally {
      setLoading(false);
    }
  }

  async function loadTeachers() {
    try {
      const res = await axios.get("/Users/teachers");
      setTeachers(res.data);
    } catch {
      alert("Failed to load teachers");
    }
  }

  async function submit(e) {
    e.preventDefault();

    if (!teacherId) {
      alert("Select teacher");
      return;
    }

    const payload = {
      title,
      description,
      teacherId
    };

    try {
      if (editingId) {
        await updateCourse(editingId, payload);
      } else {
        await createCourse(payload);
      }

      resetForm();
      load();
    } catch (e) {
      alert(e.message);
    }
  }

  function edit(course) {
    setEditingId(course.id);
    setTitle(course.title);
    setDescription(course.description || "");
    setTeacherId(course.teacherId);
  }

  async function remove(id) {
    if (!window.confirm("Delete course?")) return;
    await deleteCourse(id);
    load();
  }

  function resetForm() {
    setTitle("");
    setDescription("");
    setTeacherId("");
    setEditingId(null);
  }

  return (
    <div className="admin-courses">
      <h1>Courses</h1>

      <form className="course-form" onSubmit={submit}>
        <input
          placeholder="Title"
          value={title}
          onChange={e => setTitle(e.target.value)}
          required
        />

        <input
          placeholder="Description"
          value={description}
          onChange={e => setDescription(e.target.value)}
        />

        <select
          value={teacherId}
          onChange={e => setTeacherId(e.target.value)}
          required
        >
          <option value="">Select teacher</option>
          {teachers.map(t => (
            <option key={t.id} value={t.id}>
              {t.firstName} {t.lastName}
            </option>
          ))}
        </select>

        <button type="submit">
          {editingId ? "Update" : "Create"}
        </button>

        {editingId && (
          <button
            type="button"
            className="cancel"
            onClick={resetForm}
          >
            Cancel
          </button>
        )}
      </form>

      {loading ? (
        <p>Loading...</p>
      ) : (
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
            {courses.map(c => (
              <tr key={c.id}>
                <td>{c.title}</td>
                <td>{c.description}</td>
                <td>
                  {c.teacher?.firstName} {c.teacher?.lastName}
                </td>
                <td className="actions">
                  <button onClick={() => edit(c)}>Edit</button>
                  <button onClick={() => remove(c.id)}>Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}
