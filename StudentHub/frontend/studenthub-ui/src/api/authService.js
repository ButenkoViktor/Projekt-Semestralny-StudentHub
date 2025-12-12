const API_URL = "https://localhost:7091/api/Auth";

export async function loginRequest(email, password) {
    const res = await fetch(`${API_URL}/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password })
    });

    if (!res.ok) {
        return { success: false, error: await res.text() };
    }

    const data = await res.json();
    localStorage.setItem("token", data.token);

    return { success: true };
}


export async function registerRequest(firstName, lastName, email, password, role) {
    const res = await fetch(`${API_URL}/register`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            firstName,
            lastName,
            email,
            password,
            role   // "Student" "Teacher"
        })
    });

    if (!res.ok) {
        return { success: false, error: await res.text() };
    }

    return { success: true };
}
export function getToken() {
    return localStorage.getItem("token");
}

export function getRoles() {
    const token = getToken();
    if (!token) return [];

    try {
        if (!payload.role) return [];

        return Array.isArray(payload.role)
            ? payload.role
            : [payload.role];
    } catch {
        return [];
    }
}