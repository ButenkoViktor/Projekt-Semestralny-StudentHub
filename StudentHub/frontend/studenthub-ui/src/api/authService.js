export async function loginRequest(email, password) {
    const res = await fetch("https://localhost:7091/api/Auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password })
    });

    if (!res.ok) return { success: false };

    const data = await res.json();
    localStorage.setItem("token", data.token);

    return { success: true };
}

export function getToken() {
    return localStorage.getItem("token");
}

export function getRoles() {
    const token = getToken();
    if (!token) return [];
    const payload = JSON.parse(atob(token.split(".")[1]));
    if (!payload.role) return [];

    return Array.isArray(payload.role) ? payload.role : [payload.role];
}