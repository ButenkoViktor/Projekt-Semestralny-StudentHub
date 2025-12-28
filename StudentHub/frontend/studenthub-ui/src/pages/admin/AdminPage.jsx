import { Outlet } from "react-router-dom";

export default function AdminPage() {
  return (
    <div style={{ padding: "24px" }}>
      <Outlet />
    </div>
  );
}
