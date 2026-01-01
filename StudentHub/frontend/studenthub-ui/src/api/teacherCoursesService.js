import axios from "axios";
import { authHeader } from "../utils/authHeader";

const API_URL = "https://localhost:7091/api/courses";

export async function getMyCourses() {
  const res = await axios.get(`${API_URL}/my`, {
    headers: authHeader(),
  });
  return res.data;
}