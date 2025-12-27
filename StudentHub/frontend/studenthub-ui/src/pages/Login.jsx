import { useState, useContext, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../auth/AuthContext";
import { getRoles } from "../api/authService";
import "./styles/Login.css";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [rememberMe, setRememberMe] = useState(false);
  const [errors, setErrors] = useState({});
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  useEffect(() => {
    const savedEmail = localStorage.getItem("remember_email");
    if (savedEmail) {
      setEmail(savedEmail);
      setRememberMe(true);
    }
  }, []);

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

      if (rememberMe) {
        localStorage.setItem("remember_email", email);
      } else {
        localStorage.removeItem("remember_email");
      }

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

          <div className="remember-row">
            <label className="remember-checkbox">
              <input
                type="checkbox"
                checked={rememberMe}
                onChange={() => setRememberMe(!rememberMe)}
              />
              <span className="checkmark"></span>
              Remember me
            </label>
          </div>

          <button type="submit">Log in</button>
        </form>

        <div className="links">
          <a href="/register"> Register as a new user</a>
        </div>

        <div className="login-divider"></div>
        <div className="login-help">
          Can't log in? Send an email to{" "}
          <a href="mailto:studenthub.pl.help@gmail.com">
            studenthub.pl.help@gmail.com
          </a>
        </div>
      </div>
    </div>
  );
}
