import { useState } from "react";
import { registerRequest } from "../api/authService";
import "./styles/Login.css";

export default function Register() {
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    role: "Student"
  });
   const [errors, setErrors] = useState({});
  const [openSelect, setOpenSelect] = useState(false);
  const [showPassword, setShowPassword] = useState(false);

  function handleChange(e) {
    setForm({ ...form, [e.target.name]: e.target.value });
  }

  function validate() {
    const newErrors = {};

    if (form.firstName.trim().length < 2)
      newErrors.firstName = "First name must be at least 2 characters";

    if (form.lastName.trim().length < 2)
      newErrors.lastName = "Last name must be at least 2 characters";

    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email))
      newErrors.email = "Invalid email format";

    if (
      form.password.length < 8 ||
      !/[A-Z]/.test(form.password) ||
      !/[0-9]/.test(form.password)
    ) {
      newErrors.password =
        "Password must be 8+ chars, include uppercase letter and number";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  }

  function pickRole(role) {
    setForm({ ...form, role });
    setOpenSelect(false);
  }

  async function handleSubmit(e) {
  e.preventDefault();
  if (!validate()) return;

  const result = await registerRequest(form);

  if (result.success) {
    alert("Registration successful!");
    window.location.href = "/login";
  } else {
    alert(result.message || "Registration failed");
  }
}

async function handleSubmit(e) {
  e.preventDefault();
  if (!validate()) return;

  const result = await registerRequest(form);

  if (result.success) {
    alert("Registration successful!");
    window.location.href = "/login";
  } else {
    alert(result.message || "Registration failed");
  }
}


  return (
    <div className="login-bg">
      <div className={`login-container ${openSelect ? "expanded" : ""}`}>
        <h2>Register</h2>
        <form onSubmit={handleSubmit}>
          <input
            name="firstName"
            placeholder="First name"
            value={form.firstName}
            onChange={handleChange}
            required
          />
          {errors.firstName && <span className="error">{errors.firstName}</span>}
          <input
            name="lastName"
            placeholder="Last name"
            value={form.lastName}
            onChange={handleChange}
            required
          />
          {errors.lastName && <span className="error">{errors.lastName}</span>}

          <input
            type="email"
            name="email"
            placeholder="Email"
            value={form.email}
            onChange={handleChange}
            required
          />
          {errors.email && <span className="error">{errors.email}</span>}
           <div className="password-field">
            <input
              type={showPassword ? "text" : "password"}
              name="password"
              placeholder="Password"
              value={form.password}
              onChange={handleChange}
            />
            <i
              className={`fa-solid ${
                showPassword ? "fa-eye-slash" : "fa-eye"
              }`}
              onClick={() => setShowPassword(!showPassword)}
            />
          </div>
          {errors.password && <span className="error">{errors.password}</span>}
          <div className={`custom-select ${openSelect ? "open" : ""}`}>
            <div
              className="custom-select-header"
              onClick={() => setOpenSelect(!openSelect)}
            >
              {form.role}
            </div>

            <div className="custom-select-options">
              <div onClick={() => pickRole("Student")}>Student</div>
              <div onClick={() => pickRole("Teacher")}>Teacher</div>
            </div>
          </div>

          <button type="submit">Register</button>
          <div className="login-divider"></div>
          <div className="login-help">
          Can't register? Send an email to{" "}
          <a href="mailto:studenthub.pl.help@gmail.com">
            studenthub.pl.help@gmail.com
          </a>
        </div>
        </form>
      </div>
    </div>
  );
}