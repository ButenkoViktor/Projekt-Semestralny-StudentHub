import "./../pages/styles/Home.css";
import { Link } from "react-router-dom";

export default function Home() {
  return (
    <div className="home-container">

      {/* HERO */}
      <section className="hero">
        <h1>StudentHub</h1>
        <p>A centralized platform for students and teachers</p>

        <div className="hero-buttons">
          <Link to="/login" className="btn btn-login">Login</Link>
          <Link to="/register" className="btn btn-register">Register</Link>
        </div>
      </section>

      {/* FEATURES */}
      <section className="features">
        <div className="feature-card">
          <h3>📚 Study Materials</h3>
          <p>Access to files, materials, and resources.</p>
        </div>

        <div className="feature-card">
          <h3>📝 Communication</h3>
          <p>Discussions, messages, and announcements.</p>
        </div>

        <div className="feature-card">
          <h3>⚙️ Course Management</h3>
          <p>Create, edit, and evaluate assignments.</p>
        </div>
      </section>

      {/* FOOTER */}
      <footer className="footer">
        <p>© 2025 StudentHub. All rights reserved.</p>
      </footer>
    </div>
  );
}
