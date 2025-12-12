import { useState } from "react";
import { registerRequest } from "../api/authService";

export default function Register() {

  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    role: "Student"
  });

  function handleChange(e) {
    setForm({ ...form, [e.target.name]: e.target.value });
  }

  async function handleSubmit(e) {
    e.preventDefault();

    const result = await registerRequest(form);

    if (result.ok) {
      alert("Registration successful! Now you can login.");
      window.location.href = "/login";
    } else {
      alert(result.message);
    }
  }

  return (
    <div className="login-bg">
      <div className="login-container">
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

          <select
            name="role"
            value={form.role}
            onChange={handleChange}
            required
          >
            <option value="Student">Student</option>
            <option value="Teacher">Teacher</option>
          </select>

          <button type="submit" className="log">Register</button>
        </form>

      </div>
    </div>
  );
}