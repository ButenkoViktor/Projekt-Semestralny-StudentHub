import { useContext, useState } from "react";
import { AuthContext } from "../auth/AuthContext";

export default function Login() {
    const { login } = useContext(AuthContext);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    async function handleLogin(e) {
        e.preventDefault();
        try {
            await login(email, password);
        } catch (err) {
            alert("Wrong email or password");
        }
    }

    return (
        <form onSubmit={handleLogin}>
            <input placeholder="Email" onChange={(e) => setEmail(e.target.value)} />
            <input
                placeholder="Password"
                type="password"
                onChange={(e) => setPassword(e.target.value)}
            />
            <button>Login</button>
        </form>
    );
}