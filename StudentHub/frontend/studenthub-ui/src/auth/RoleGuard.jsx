import { getRoles } from "../api/authService";
import { Navigate } from "react-router-dom";

export default function RoleGuard({ children, requiredRole }) {

    let roles = getRoles();

    if (!roles.includes(requiredRole)) {
        return <Navigate to="/dashboard" />;
    }

    return children;
}