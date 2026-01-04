import { useEffect, useState } from "react";
import api from "../../api/axios";
import RoleSelect from "../../components/RoleSelect";
import "./AdminUsers.css";

export default function AdminUsers() {
  const [users, setUsers] = useState([]);
  const [filter, setFilter] = useState("All");
  const [editingEmail, setEditingEmail] = useState(null);
  const [newEmail, setNewEmail] = useState("");

  const loadData = async () => {
    const res = await api.get("/admin/users");
    setUsers(res.data);
  };

  useEffect(() => {
    loadData();
  }, []);

  const assignRole = async (userId, role) => {
    await api.post("/admin/users/assign-role", { userId, role });
    loadData();
  };

  const removeRole = async (userId, role) => {
    await api.post("/admin/users/remove-role", { userId, role });
    loadData();
  };

  const updateEmail = async (userId) => {
    await api.put("/admin/users/update-email", {
      userId,
      newEmail,
    });
    setEditingEmail(null);
    setNewEmail("");
    loadData();
  };

  const filteredUsers =
    filter === "All"
      ? users
      : users.filter((u) => u.roles.includes(filter));

  return (
    <div className="admin-users">
      <h1>Admin → Users</h1>

      <div className="admin-users-toolbar">
        <label>Filter:</label>
        <select value={filter} onChange={(e) => setFilter(e.target.value)}>
          <option>All</option>
          <option>Admin</option>
          <option>Teacher</option>
          <option>Student</option>
        </select>
      </div>

      <table>
        <thead>
          <tr>
            <th>Email</th>
            <th>Name</th>
            <th>Roles</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {filteredUsers.map((u) => (
            <tr key={u.id}>
              <td>
                {editingEmail === u.id ? (
                  <div className="email-edit">
                    <input
                      value={newEmail}
                      onChange={(e) => setNewEmail(e.target.value)}
                    />
                    <button
                      className="action-btn"
                      onClick={() => updateEmail(u.id)}
                    >
                      Save
                    </button>
                  </div>
                ) : (
                  <>
                    {u.email}
                    <button
                      className="action-btn"
                      style={{ marginLeft: 8 }}
                      onClick={() => {
                        setEditingEmail(u.id);
                        setNewEmail(u.email);
                      }}
                    >
                      Edit
                    </button>
                  </>
                )}
              </td>

              <td>
                {u.firstName} {u.lastName}
              </td>

              <td>
                {u.roles.map((r) => (
                  <span key={r} className="role-badge">
                    {r}
                  </span>
                ))}
              </td>

              <td>
                <RoleSelect
                  roles={["Admin", "Teacher", "Student"]}
                  userRoles={u.roles}
                  onAssign={(role) => assignRole(u.id, role)}
                  onRemove={(role) => removeRole(u.id, role)}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
