import { createContext, useState, useEffect } from "react";
import { loginRequest, getToken } from "../api/authService";

export const AuthContext = createContext();

export function AuthProvider({ children }) {
    const [user, setUser] = useState(null);

    const login = async (email, password) => {
        const result = await loginRequest(email, password);
        if (result.success) await fetchUser();
        else throw new Error("Invalid credentials");
    };

    const logout = () => {
        localStorage.removeItem("token");
        setUser(null);
    };

    async function fetchUser() {
        const token = getToken();
        if (!token) return;

        const res = await fetch("https://localhost:7091/api/User/me", {
            headers: { Authorization: `Bearer ${token}` }
        });

        if (res.ok) {
            const data = await res.json();
            setUser(data);
        }
    }

    useEffect(() => {
        fetchUser();
    }, []);

    return (
        <AuthContext.Provider value={{ user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
}