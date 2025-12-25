import * as signalR from "@microsoft/signalr";
import { getToken } from "../api/authService";

let connection;

export function startPresenceConnection(onOnline, onOffline) {
  connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7091/hubs/presence", {
      accessTokenFactory: () => getToken(),
      withCredentials: true
    })
    .withAutomaticReconnect()
    .build();

  connection.on("UserOnline", userId => {
    onOnline(userId);
  });

  connection.on("UserOffline", userId => {
    onOffline(userId);
  });

  connection
    .start()
    .then(() => console.log("🟢 Presence connected"))
    .catch(err => console.error("Presence error", err));
}