import { Routes, Route, useLocation } from "react-router-dom";
import { useState } from "react";

import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";

import AdminPage from "./pages/admin/AdminPage";
import TeacherPage from "./pages/teacher/TeacherPage";
import StudentPage from "./pages/student/StudentPage";

import AdminHome from "./pages/admin/AdminHome";
import AdminUsers from "./pages/admin/AdminUsers";
import AdminCourses from "./pages/admin/AdminCourses";
import AdminGroups from "./pages/admin/AdminGroups";
import AdminEvents from "./pages/admin/AdminEvents";

import StudentHome from "./pages/student/StudentHome";
import StudentCourses from "./pages/student/StudentCourses";
import StudentTasks from "./pages/student/StudentTasks";
import StudentSchedule from "./pages/student/StudentSchedule";
import StudentNotes from "./pages/student/StudentNotes";

import TeacherHome from "./pages/teacher/TeacherHome";
import TeacherCourses from "./pages/teacher/TeacherCourses";
import TeacherGroups from "./pages/teacher/TeacherGroups";
import TeacherTasks from "./pages/teacher/TeacherTasks";
import TeacherSchedule from "./pages/teacher/TeacherSchedule";

import ProtectedRoute from "./auth/ProtectedRoute";
import RoleGuard from "./auth/RoleGuard";
import Navbar from "./components/Navbar";
import ProfilePage from "./pages/ProfilePage";

import ChatButton from "./chat/ChatButton";
import ChatWindow from "./chat/ChatWindow";

function App() {
  const location = useLocation();
  const [chatOpen, setChatOpen] = useState(false);
  const [unreadCount, setUnreadCount] = useState(0);
  const hideNavbar = ["/", "/login", "/register"].includes(location.pathname);
  const hideChat = ["/", "/login", "/register"].includes(location.pathname);

  return (
    <>
      {!hideNavbar && <Navbar />}

      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/profile" element={<ProfilePage />} />

        <Route
          path="/admin"
          element={
            <ProtectedRoute>
              <RoleGuard requiredRole="Admin">
                <AdminPage />
              </RoleGuard>
            </ProtectedRoute>
          }
        >
          <Route index element={<AdminHome />} />
          <Route path="users" element={<AdminUsers />} />
          <Route path="courses" element={<AdminCourses />} />
          <Route path="groups" element={<AdminGroups />} />
          <Route path="events" element={<AdminEvents  />} />
        </Route>

        <Route
          path="/teacher"
          element={
            <ProtectedRoute>
              <RoleGuard requiredRole="Teacher">
                <TeacherPage />
              </RoleGuard>
            </ProtectedRoute>
          }
        >
          <Route index element={<TeacherHome />} />
          <Route path="courses" element={<TeacherCourses />} />
          <Route path="groups" element={<TeacherGroups />} />
          <Route path="tasks" element={<TeacherTasks />} />
          <Route path="schedule" element={<TeacherSchedule />} />
        </Route>

        <Route
          path="/student"
          element={
            <ProtectedRoute>
              <RoleGuard requiredRole="Student">
                <StudentPage />
              </RoleGuard>
            </ProtectedRoute>
          }
        >
          <Route index element={<StudentHome />} />
          <Route path="courses" element={<StudentCourses />} />
          <Route path="tasks" element={<StudentTasks />} />
          <Route path="schedule" element={<StudentSchedule />} />
          <Route path="notes" element={<StudentNotes />} />
        </Route>
      </Routes>

      {!hideChat && (
        <>
          {chatOpen && <ChatWindow onClose={() => setChatOpen(false)} />}
          <ChatButton
          onClick={() => {setUnreadCount(0); setChatOpen(true); }}
            unreadCount={unreadCount}
          />
        </>
      )}
    </>
  );
}

export default App;