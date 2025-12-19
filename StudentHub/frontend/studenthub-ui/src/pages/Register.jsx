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

  const [openSelect, setOpenSelect] = useState(false);

  function handleChange(e) {
    setForm({ ...form, [e.target.name]: e.target.value });
  }

  function pickRole(role) {
    setForm({ ...form, role });
    setOpenSelect(false);
  }

  async function handleSubmit(e) {
    e.preventDefault();

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

          <input
            name="lastName"
            placeholder="Last name"
            value={form.lastName}
            onChange={handleChange}
            required
          />

          <input
            type="email"
            name="email"
            placeholder="Email"
            value={form.email}
            onChange={handleChange}
            required
          />

          <input
            type="password"
            name="password"
            placeholder="Password"
            value={form.password}
            onChange={handleChange}
            required
          />

          {/* ROLE SELECT */}
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
        </form>
      </div>
    </div>
  );
}
