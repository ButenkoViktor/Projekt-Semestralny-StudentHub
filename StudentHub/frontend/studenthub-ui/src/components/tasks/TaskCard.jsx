export default function TaskCard({ task, onEdit, onDelete }) {
  return (
    <div className="task-card">
      <h3>{task.title}</h3>
      <p>{task.description}</p>

      <div className="task-meta">
        <span>📅 {new Date(task.deadline).toLocaleDateString()}</span>
        <span>📘 Course ID: {task.courseId}</span>
      </div>

      <div className="task-actions">
        <button onClick={() => onEdit(task)}>Edit</button>
        <button className="danger" onClick={() => onDelete(task.id)}>
          Delete
        </button>
      </div>
    </div>
  );
}