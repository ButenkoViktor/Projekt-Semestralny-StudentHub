import { useEffect, useState } from "react";
import { getAdminDashboard } from "../../api/adminService";

export default function AdminHome() {
  const [stats, setStats] = useState({
    users: 0,
    courses: 0,
    groups: 0,
    schedules: 0,
  });

  useEffect(() => {
    loadStats();
  }, []);

  const loadStats = async () => {
    try {
      const data = await getAdminDashboard();
      setStats(data);
    } catch (err) {
      console.error("Failed to load dashboard stats", err);
    }
  };

  return (
    <div style={pageStyle}>
      <h1 style={titleStyle}>Admin Dashboard</h1>

      <div style={gridStyle}>
        <StatCard title="Users" value={stats.users} />
        <StatCard title="Courses" value={stats.courses} />
        <StatCard title="Groups" value={stats.groups} />
      </div>
    </div>
  );
}

function StatCard({ title, value }) {
  return (
    <div style={cardStyle}>
      <span style={cardTitle}>{title}</span>
      <span style={cardValue}>{value}</span>
    </div>
  );
}



const pageStyle = {
  padding: "32px",
};

const titleStyle = {
  fontSize: "28px",
  fontWeight: "900",
  marginBottom: "24px",
};

const gridStyle = {
  display: "grid",
  gridTemplateColumns: "repeat(auto-fit, minmax(220px, 1fr))",
  gap: "24px",
};

const cardStyle = {
  background: "linear-gradient(135deg, #ffffff, #f9fafb)",
  width: "300px",
  borderRadius: "16px",
  padding: "30px 20px",
  boxShadow: "0 10px 30px rgba(0,0,0,0.06)",
  display: "flex",
  flexDirection: "column",
  alignItems: "flex-start",
  transition: "transform .2s ease",
};

const cardTitle = {
  fontSize: "14px",
  color: "#6b7280",
  textTransform: "uppercase",
  letterSpacing: "0.05em",
};

const cardValue = {
  fontSize: "36px",
  fontWeight: "500",
  marginTop: "12px",
  color: "#111827",
};
