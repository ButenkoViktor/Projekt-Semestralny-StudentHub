import { useContext, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { AuthContext } from "../auth/AuthContext";
import { getRoles } from "../api/authService";
import "./Navbar.css";

export default function Navbar() {
  const { user, logout } = useContext(AuthContext);
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);
  const roles = getRoles();

  function handleLogout() {
    logout();
    navigate("/login");
  }

  function renderLinks() {
    if (roles.includes("Admin")) {
      return (
        <>
          <NavItem to="/admin">Admin</NavItem>
          <NavItem to="/admin/users">Users</NavItem>
          <NavItem to="/admin/courses">Courses</NavItem>
          <NavItem to="/admin/groups">Groups</NavItem>
          <NavItem to="/admin/events">Events</NavItem>
        </>
      );
    }

    if (roles.includes("Teacher")) {
      return (
        <>
          <NavItem to="/teacher">Home</NavItem>
          <NavItem to="/teacher/courses">Courses</NavItem>
          <NavItem to="/teacher/groups">Groups</NavItem>
          <NavItem to="/teacher/tasks">Tasks</NavItem>
          <NavItem to="/teacher/schedule">Schedule</NavItem>
        </>
      );
    }

    return (
      <>
        <NavItem to="/student">Home</NavItem>
        <NavItem to="/student/courses">Courses</NavItem>
        <NavItem to="/student/tasks">Tasks</NavItem>
        <NavItem to="/student/schedule">Schedule</NavItem>
        <NavItem to="/student/notes">Notes</NavItem>
      </>
    );
  }

  return (
    <nav className="navbar">
      <div className="navbar-left">
        <span className="navbar-logo">StudentHub</span>
        <div className="navbar-links">
          {renderLinks()}
        </div>
      </div>

      <div className="navbar-right">
        <div className="profile" onClick={() => setOpen(!open)}>
          <div className="profile-avatar">
            {user?.firstName?.[0] || "U"}
          </div>
        </div>

        {open && (
          <div className="profile-menu">
            <div className="profile-name">
              {user?.firstName} {user?.lastName}
            </div>

            <Link to="/profile">My profile</Link>
            <button onClick={handleLogout}>Logout</button>
          </div>
        )}
      </div>
    </nav>
  );
}

function NavItem({ to, children }) {
  return (
    <Link to={to} className="nav-item">
      {children}
    </Link>
  );
}
