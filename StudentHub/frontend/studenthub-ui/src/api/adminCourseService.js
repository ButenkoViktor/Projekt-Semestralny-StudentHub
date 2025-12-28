const API = "https://localhost:7091/api/Courses";

async function authFetch(url, options = {}) {
  const token = localStorage.getItem("token");

  const res = await fetch(url, {
    ...options,
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
      ...(options.headers || {})
    }
  });

  if (!res.ok) {
    const text = await res.text();
    throw new Error(text || "API error");
  }

  return res.status === 204 ? null : res.json();
}

export const getAllCourses = () =>
  authFetch(API);

export const createCourse = (data) =>
  authFetch(API, {
    method: "POST",
    body: JSON.stringify(data)
  });

export const updateCourse = (id, data) =>
  authFetch(`${API}/${id}`, {
    method: "PUT",
    body: JSON.stringify(data)
  });

export const deleteCourse = (id) =>
  authFetch(`${API}/${id}`, {
    method: "DELETE"
  });

export const getTeachers = () =>
  authFetch("https://localhost:7091/api/Users/teachers");

