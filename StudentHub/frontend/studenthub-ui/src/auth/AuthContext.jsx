import { createContext, useState, useEffect } from "react";
import { loginRequest, getToken } from "../api/authService";

export const AuthContext = createContext();

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  const login = async (email, password) => {
    const result = await loginRequest(email, password);
    if (!result.success) throw new Error("Invalid credentials");
    await fetchUser();
  };

  const logout = () => {
    localStorage.removeItem("token");
    setUser(null);
  };

  async function fetchUser() {
    const token = getToken();
    if (!token) {
      setLoading(false);
      return;
    }

    const res = await fetch("https://localhost:7091/api/User/me", {
      headers: { Authorization: `Bearer ${token}` }
    });

    if (res.ok) {
      const data = await res.json();
      setUser(data);
    }

    setLoading(false);
  }

  useEffect(() => {
    fetchUser();
  }, []);

  return (
    <AuthContext.Provider value={{ user, login, logout, loading }}>
      {children}
    </AuthContext.Provider>
  );
}