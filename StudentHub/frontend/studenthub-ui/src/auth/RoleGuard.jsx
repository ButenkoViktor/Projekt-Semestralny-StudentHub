import { Navigate } from "react-router-dom";
import { getRoles } from "../api/authService";

export default function RoleGuard({ children, requiredRole }) {
  const roles = getRoles();

  if (!roles.includes(requiredRole)) {
    return <Navigate to="/login" />;
  }

  return children;
}