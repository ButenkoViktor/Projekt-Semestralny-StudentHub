import { getRoles } from "../api/authService";

export default function Dashboard() {
    const roles = getRoles();

    return (
        <div>
            <h1>Dashboard</h1>
            <p>Your roles: {roles.join(", ")}</p>
        </div>
    );
}
