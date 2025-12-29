import { useEffect, useState } from "react";
import { getMyProfile, updateEmail } from "../api/profileService";
import "./styles/ProfilePage.css";

export default function ProfilePage() {
  const [user, setUser] = useState(null);
  const [newEmail, setNewEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(true);
  const [message, setMessage] = useState(null);

  useEffect(() => {
    async function load() {
      try {
        const data = await getMyProfile();
        setUser(data);
        setNewEmail(data.email);
      } catch {
        setMessage("Failed to load profile");
      } finally {
        setLoading(false);
      }
    }
    load();
  }, []);

  const handleEmailChange = async e => {
    e.preventDefault();
    setMessage(null);

    try {
      await updateEmail({ newEmail, password });
      setUser({ ...user, email: newEmail });
      setPassword("");
      setMessage("✅ Email updated successfully");
    } catch {
      setMessage("❌ Wrong password or email already used");
    }
  };

  if (loading) return <div className="profile-loading">Loading profile...</div>;

  return (
    <div className="profile-page">
      <section className="profile-card">
        <h1>My Profile</h1>

        <div className="profile-grid">
          <ProfileItem label="First name" value={user.firstName} />
          <ProfileItem label="Last name" value={user.lastName} />
          <ProfileItem label="Role" value={user.roles?.join(", ")} />
          <ProfileItem label="User ID" value={user.id} mono />
        </div>
      </section>

      <section className="profile-card">
        <h2>Change email</h2>

        <form onSubmit={handleEmailChange} className="profile-form">
          <label>
            New email
            <input
              type="email"
              value={newEmail}
              onChange={e => setNewEmail(e.target.value)}
              required
            />
          </label>

          <label>
            Current password
            <input
              type="password"
              value={password}
              onChange={e => setPassword(e.target.value)}
              required
            />
          </label>

          <button type="submit">Update email</button>

          {message && <p className="profile-message">{message}</p>}
        </form>
      </section>
    </div>
  );
}

function ProfileItem({ label, value, mono }) {
  return (
    <div className="profile-item">
      <span>{label}</span>
      <strong className={mono ? "mono" : ""}>{value}</strong>
    </div>
  );
}
