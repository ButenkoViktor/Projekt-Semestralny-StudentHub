export default function AdminHome() {
  return (
    <>
      <h1>Admin Dashboard</h1>

      <div style={{ display: "flex", gap: "20px", marginTop: "20px" }}>
        <StatCard title="Users" />
        <StatCard title="Courses" />
        <StatCard title="Groups" />
      </div>
    </>
  );
}

function StatCard({ title }) {
  return (
    <div style={{
      background: "#fff",
      padding: "24px",
      borderRadius: "12px",
      width: "220px",
      boxShadow: "0 5px 20px rgba(0,0,0,.05)"
    }}>
      <h3>{title}</h3>
      <p style={{ fontSize: "32px", marginTop: "10px" }}>0</p>
    </div>
  );
}
