import { createContext, useState, useEffect } from "react";

export const AuthContext = createContext();

export function AuthProvider({ children }) {
    const [user, setUser] = useState(null);
    const login = async (email, password) => {
        const res = await fetch("http://localhost:5174/api/auth/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password })
        });

        if (!res.ok) {
            throw new Error("Invalid credentials");
        }

        const data = await res.json();

        localStorage.setItem("token", data.token);

        await fetchUser(); 
    };

  
    const logout = () => {
        localStorage.removeItem("token");
        setUser(null);
    };

 
    async function fetchUser() {
        const token = localStorage.getItem("token");
        if (!token) return;

        const res = await fetch("http://localhost:5174/api/user/me", {
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