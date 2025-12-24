import { Outlet } from "react-router-dom";

export default function TeacherPage() {
  return (
    <div className="teacher-page">
      <Outlet />
    </div>
  );
}