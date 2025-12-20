import { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../auth/AuthContext";
import { getRoles } from "../api/authService";
import "./styles/Login.css";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [errors, setErrors] = useState({});
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  function validate() {
    const newErrors = {};
    if (!email) newErrors.email = "Email is required";
    if (!password) newErrors.password = "Password is required";
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  }

  async function handleLogin(e) {
    e.preventDefault();
    if (!validate()) return;

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
          />
          {errors.email && <span className="error">{errors.email}</span>}

          <div className="password-field">
            <input
              type={showPassword ? "text" : "password"}
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <i
              className={`fa-solid ${
                showPassword ? "fa-eye-slash" : "fa-eye"
              }`}
              onClick={() => setShowPassword(!showPassword)}
            />
          </div>
          {errors.password && <span className="error">{errors.password}</span>}

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