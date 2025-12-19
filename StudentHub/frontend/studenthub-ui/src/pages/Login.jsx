import { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../auth/AuthContext";
import { getRoles } from "../api/authService";
import "./styles/Login.css";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  async function handleLogin(e) {
    e.preventDefault();

    try {
      await login(email, password);

      const roles = getRoles();
      if (roles.includes("Admin")) navigate("/admin");
      else if (roles.includes("Teacher")) navigate("/teacher");
      else navigate("/student");
    } catch {
      alert("Login failed");
    }
  }

  return (
    <div className="login-bg">
      <div className="login-container">
        <h2>Log in</h2>

        <form onSubmit={handleLogin}>
          <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />

          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />

          <button type="submit">Log in</button>
        </form>

        <div className="links">
          <a href="/forgot">Forgot your password?</a>
          <a href="/register">Register as a new user</a>
          <a href="/resend">Resend email confirmation</a>
        </div>
      </div>
    </div>
  );
}