import { useEffect, useState } from "react";
import api from "../../api/axios";
import {
  getGroups,
  createGroup,
  assignTeacherToGroup,
  addStudentToGroup,
} from "../../api/groupsAdmin";
import "./AdminGroups.css";

export default function AdminGroups() {
  const [groups, setGroups] = useState([]);
  const [teachers, setTeachers] = useState([]);
  const [students, setStudents] = useState([]);

  const [groupName, setGroupName] = useState("");
  const [groupId, setGroupId] = useState("");
  const [teacherId, setTeacherId] = useState("");
  const [studentId, setStudentId] = useState("");

  const loadData = async () => {
    const groupsData = await getGroups();
    const users = (await api.get("/admin/users")).data;

    setGroups(groupsData);
    setTeachers(users.filter(u => u.roles.includes("Teacher")));
    setStudents(users.filter(u => u.roles.includes("Student")));
  };

  useEffect(() => {
    loadData();
  }, []);

  const create = async () => {
    if (!groupName) return alert("Group name required");
    await createGroup(groupName);
    setGroupName("");
    loadData();
  };

  const assignTeacher = async () => {
    if (!groupId || !teacherId)
      return alert("Select group and teacher");

    await assignTeacherToGroup(Number(groupId), teacherId);
    alert("Teacher assigned");
    loadData();
  };

  const addStudent = async () => {
    if (!groupId || !studentId)
      return alert("Select group and student");

    await addStudentToGroup(Number(groupId), studentId);
    alert("Student added");
    loadData();
  };

  return (
    <div className="groups-page">
      <h1>Admin → Groups</h1>

      {/* CREATE GROUP */}
      <div className="card">
        <h3>Create group</h3>
        <input
          placeholder="Group name"
          value={groupName}
          onChange={e => setGroupName(e.target.value)}
        />
        <button onClick={create}>Create</button>
      </div>

      {/* ASSIGN TEACHER */}
      <div className="card">
        <h3>Assign teacher</h3>

        <select value={groupId} onChange={e => setGroupId(e.target.value)}>
          <option value="">Select group</option>
          {groups.map(g => (
            <option key={g.id} value={g.id}>{g.name}</option>
          ))}
        </select>

        <select value={teacherId} onChange={e => setTeacherId(e.target.value)}>
          <option value="">Select teacher</option>
          {teachers.map(t => (
            <option key={t.id} value={t.id}>
              {t.firstName} {t.lastName}
            </option>
          ))}
        </select>

        <button onClick={assignTeacher}>Assign</button>
      </div>

      {/* ADD STUDENT */}
      <div className="card">
        <h3>Add student</h3>

        <select value={groupId} onChange={e => setGroupId(e.target.value)}>
          <option value="">Select group</option>
          {groups.map(g => (
            <option key={g.id} value={g.id}>{g.name}</option>
          ))}
        </select>

        <select value={studentId} onChange={e => setStudentId(e.target.value)}>
          <option value="">Select student</option>
          {students.map(s => (
            <option key={s.id} value={s.id}>
              {s.firstName} {s.lastName}
            </option>
          ))}
        </select>

        <button onClick={addStudent}>Add</button>
      </div>
    </div>
  );
}
