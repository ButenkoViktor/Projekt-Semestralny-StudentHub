import { Link } from "react-router-dom";
import { getToken, getRoles } from "../api/authService";

export default function Navbar() {
    const logged = !!getToken();
    const roles = getRoles();

    return (
        <nav>
            <Link to="/">Home</Link>

            {!logged && <Link to="/login">Login</Link>}

            {logged && <Link to="/dashboard">Dashboard</Link>}

            {roles.includes("Admin") && <Link to="/admin">Admin Panel</Link>}

            {roles.includes("Teacher") && <Link to="/teacher">Teacher Tools</Link>}
        </nav>
    );
}