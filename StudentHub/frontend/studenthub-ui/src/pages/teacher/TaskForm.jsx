import { useEffect, useState } from "react";

export default function TaskForm({ onSubmit, editingTask }) {
  const [form, setForm] = useState({
    title: "",
    description: "",
    deadline: "",
    courseId: "",
    groupId: "",
  });

  useEffect(() => {
    if (editingTask) {
      setForm({
        ...editingTask,
        deadline: editingTask.deadline.split("T")[0],
      });
    }
  }, [editingTask]);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const submit = (e) => {
    e.preventDefault();
    onSubmit(form);
    setForm({
      title: "",
      description: "",
      deadline: "",
      courseId: "",
      groupId: "",
    });
  };

  return (
    <form className="task-form" onSubmit={submit}>
      <h2>{editingTask ? "Edit Task" : "Create Task"}</h2>

      <input name="title" placeholder="Title" value={form.title} onChange={handleChange} required />
      <textarea name="description" placeholder="Description" value={form.description} onChange={handleChange} />
      <input type="date" name="deadline" value={form.deadline} onChange={handleChange} required />
      <input name="courseId" placeholder="Course ID" value={form.courseId} onChange={handleChange} required />
      <input name="groupId" placeholder="Group ID (optional)" value={form.groupId} onChange={handleChange} />

      <button type="submit">
        {editingTask ? "Update" : "Create"}
      </button>
    </form>
  );
}