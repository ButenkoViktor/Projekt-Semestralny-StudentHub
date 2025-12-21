import { Outlet } from "react-router-dom";
import "./StudentLayout.css";
export default function StudentPage() {
  return (
    <div className="student-layout">
      <main className="student-content">
        <Outlet />
      </main>
    </div>
  );
}