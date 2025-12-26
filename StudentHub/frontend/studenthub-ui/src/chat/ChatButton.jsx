export default function ChatButton({ onClick, unreadCount }) {
  return (
    <div className="chat-fab-wrapper">
      <button className="chat-fab" onClick={onClick}>
        💬
      </button>

      {unreadCount > 0 && (
        <div className="unread-badge">
          {unreadCount}
        </div>
      )}
    </div>
  );
}