import axios from "./axios";

export const getMyProfile = async () => {
  const res = await axios.get("/User/me");
  return res.data;
};

export const updateEmail = async ({ newEmail, password }) => {
  await axios.put("/User/change-email", {
    newEmail,
    password
  });
};