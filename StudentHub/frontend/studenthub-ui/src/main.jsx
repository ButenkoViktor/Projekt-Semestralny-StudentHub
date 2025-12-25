import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.jsx";
import { BrowserRouter } from "react-router-dom";
import { AuthProvider } from "./auth/AuthContext.jsx";
import { PresenceProvider } from "./chat/PresenceContext";

ReactDOM.createRoot(document.getElementById("root")).render(
  <BrowserRouter>
    <AuthProvider>
      <PresenceProvider>
        <App />
      </PresenceProvider>
    </AuthProvider>
  </BrowserRouter>
);