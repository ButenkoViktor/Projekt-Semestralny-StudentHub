import { useEffect, useState } from "react";
import api from "../../api/axios";
import { assignGroupToCourse } from "../../api/coursesAdmin";
import "./AdminCourses.css";

export default function AdminCourses() {
  const [courses, setCourses] = useState([]);
  const [teachers, setTeachers] = useState([]);
  const [groups, setGroups] = useState([]);

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [teacherId, setTeacherId] = useState("");

  const [courseId, setCourseId] = useState("");
  const [groupId, setGroupId] = useState("");

  const loadData = async () => {
    const [coursesRes, usersRes, groupsRes] = await Promise.all([
      api.get("/courses"),
      api.get("/admin/users"),
      api.get("/admin/groups"),
    ]);

    setCourses(coursesRes.data);
    setTeachers(usersRes.data.filter(u => u.roles.includes("Teacher")));
    setGroups(groupsRes.data);
  };

  useEffect(() => {
    loadData();
  }, []);

  const createCourse = async () => {
    if (!title || !teacherId)
      return alert("Title and teacher are required");

    await api.post("/courses", {
      title,
      description,
      teacherId,
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

  const assignGroup = async () => {
    if (!courseId || !groupId)
      return alert("Select course and group");

    await assignGroupToCourse(Number(courseId), Number(groupId));
    alert("Group assigned");
    loadData();
  };

  const teacherMap = Object.fromEntries(
    teachers.map(t => [t.id, `${t.firstName} ${t.lastName}`])
  );

  return (
    <div className="courses-page">
      <h1>Admin → Courses</h1>

      <div className="card">
        <h3>Create course</h3>

        <input
          placeholder="Title"
          value={title}
          onChange={e => setTitle(e.target.value)}
        />

        <textarea
          placeholder="Description"
          value={description}
          onChange={e => setDescription(e.target.value)}
        />

        <select value={teacherId} onChange={e => setTeacherId(e.target.value)}>
          <option value="">Select teacher</option>
          {teachers.map(t => (
            <option key={t.id} value={t.id}>
              {t.firstName} {t.lastName}
            </option>
          ))}
        </select>

        <button onClick={createCourse}>Create</button>
      </div>

      <div className="card">
        <h3>Assign group to course</h3>

        <select value={courseId} onChange={e => setCourseId(e.target.value)}>
          <option value="">Select course</option>
          {courses.map(c => (
            <option key={c.id} value={c.id}>{c.title}</option>
          ))}
        </select>

        <select value={groupId} onChange={e => setGroupId(e.target.value)}>
          <option value="">Select group</option>
          {groups.map(g => (
            <option key={g.id} value={g.id}>{g.name}</option>
          ))}
        </select>

        <button onClick={assignGroup}>Assign</button>
      </div>

      <div className="table-wrapper">
        <table className="courses-table">
          <thead>
            <tr>
              <th>Title</th>
              <th>Description</th>
              <th>Teacher</th>
              <th>Groups</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {courses.map(c => (
              <tr key={c.id}>
                <td>{c.title}</td>
                <td className="description">{c.description || "—"}</td>
                <td>{teacherMap[c.teacherId] || "—"}</td>
                <td>
                  {c.groups?.length
                    ? c.groups.map(g => g.name).join(", ")
                    : "—"}
                </td>
                <td>
                  <button
                    className="delete-btn"
                    onClick={() => deleteCourse(c.id)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
