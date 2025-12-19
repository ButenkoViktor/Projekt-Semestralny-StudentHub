import { Routes, Route, useLocation } from "react-router-dom";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";

import AdminPage from "./pages/admin/AdminPage";
import TeacherPage from "./pages/teacher/TeacherPage";
import StudentPage from "./pages/student/StudentPage";

import ProtectedRoute from "./auth/ProtectedRoute";
import RoleGuard from "./auth/RoleGuard";
import Navbar from "./components/Navbar";

function App() {
  const location = useLocation();
  const hideNavbar = ["/", "/login", "/register"].includes(location.pathname);

  return (
    <>
      {!hideNavbar && <Navbar />}

      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />

        <Route
          path="/admin"
          element={
            <ProtectedRoute>
              <RoleGuard requiredRole="Admin">
                <AdminPage />
              </RoleGuard>
            </ProtectedRoute>
          }
        />

        <Route
          path="/teacher"
          element={
            <ProtectedRoute>
              <RoleGuard requiredRole="Teacher">
                <TeacherPage />
              </RoleGuard>
            </ProtectedRoute>
          }
        />

        <Route
          path="/student"
          element={
            <ProtectedRoute>
              <RoleGuard requiredRole="Student">
                <StudentPage />
              </RoleGuard>
            </ProtectedRoute>
          }
        />
      </Routes>
    </>
  );
}

export default App;