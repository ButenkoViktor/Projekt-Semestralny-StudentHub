export default function RoleSelect({ roles, userRoles, onAssign, onRemove }) {
  return (
    <div>
      {roles.map((role) =>
        userRoles.includes(role) ? (
          <button
            key={role}
            onClick={() => onRemove(role)}
            style={{ marginRight: 4 }}
          >
            Remove {role}
          </button>
        ) : (
          <button
            key={role}
            onClick={() => onAssign(role)}
            style={{ marginRight: 4 }}
          >
            Add {role}
          </button>
        )
      )}
    </div>
  );
}

const btn = {
  padding: "4px 8px",
  borderRadius: "6px",
  border: "1px solid #ddd",
  background: "#e0f2fe",
  cursor: "pointer",
};