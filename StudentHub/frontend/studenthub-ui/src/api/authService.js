let API_URL = "http://localhost:5174/api/auth";

export function getToken() {
    return localStorage.getItem("token");
}

export function getRoles() {
    let roles = localStorage.getItem("roles");
    return roles ? JSON.parse(roles) : [];
}

export async function login(email, password) {
    const response = await fetch(`${API_URL}/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password })
    });

    if (!response.ok) throw new Error("Invalid login");

    return await response.json();
}