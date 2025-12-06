import { useState } from "react";
import { login } from "../api/authService";
import { useNavigate } from "react-router-dom";

export default function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    async function handleLogin(e) {
        e.preventDefault();
        try {
            let res = await login(email, password);

            localStorage.setItem("token", res.token);

            navigate("/dashboard");
        } catch (err) {
            alert("Wrong email or password");
        }
    }

    return (
        <div>
            <h2>Login</h2>
            <form onSubmit={handleLogin}>
                <input placeholder="Email"
                       onChange={(e) => setEmail(e.target.value)} />
                <input placeholder="Password"
                       type="password"
                       onChange={(e) => setPassword(e.target.value)} />
                <button>Login</button>
            </form>
        </div>
    );
}