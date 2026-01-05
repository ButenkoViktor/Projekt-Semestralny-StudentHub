import { useEffect, useState } from "react";
import api from "../../api/axios";
import {
  getGroups,
  createGroup,
  assignTeacherToGroup,
  addStudentToGroup
} from "../../api/groupsAdmin";
import "./AdminGroups.css";

export default function AdminGroups() {
  const [groups, setGroups] = useState([]);
  const [teachers, setTeachers] = useState([]);
  const [students, setStudents] = useState([]);

  const [groupName, setGroupName] = useState("");
  const [selectedGroup, setSelectedGroup] = useState("");
  const [selectedTeacher, setSelectedTeacher] = useState("");
  const [selectedStudent, setSelectedStudent] = useState("");

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

        <select onChange={e => setSelectedGroup(e.target.value)}>
          <option value="">Select group</option>
          {groups.map(g => (
            <option key={g.id} value={g.id}>{g.name}</option>
          ))}
        </select>

        <select onChange={e => setSelectedTeacher(e.target.value)}>
          <option value="">Select teacher</option>
          {teachers.map(t => (
            <option key={t.id} value={t.id}>
              {t.firstName} {t.lastName}
            </option>
          ))}
        </select>

        <button
          onClick={() =>
            assignTeacherToGroup(selectedGroup, selectedTeacher)
          }
        >
          Assign
        </button>
      </div>

      {/* ADD STUDENT */}
      <div className="card">
        <h3>Add student</h3>

        <select onChange={e => setSelectedGroup(e.target.value)}>
          <option value="">Select group</option>
          {groups.map(g => (
            <option key={g.id} value={g.id}>{g.name}</option>
          ))}
        </select>

        <select onChange={e => setSelectedStudent(e.target.value)}>
          <option value="">Select student</option>
          {students.map(s => (
            <option key={s.id} value={s.id}>
              {s.firstName} {s.lastName}
            </option>
          ))}
        </select>

        <button
          onClick={() =>
            addStudentToGroup(selectedGroup, selectedStudent)
          }
        >
          Add
        </button>
      </div>
    </div>
  );
}
