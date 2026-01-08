import { useEffect, useState } from "react";
import {
  getMyNotes,
  createNote,
  updateNote,
  deleteNote
} from "../../api/notesService";

import "./StudentNotes.css";

export default function StudentNotes() {
  const [notes, setNotes] = useState([]);
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [editingId, setEditingId] = useState(null);

  const loadNotes = async () => {
    const data = await getMyNotes();
    setNotes(data);
  };

  useEffect(() => {
    loadNotes();
  }, []);

  const handleSave = async () => {
    if (!title.trim() || !content.trim()) {
      alert("Title and content are required");
      return;
    }

    if (editingId) {
      await updateNote(editingId, { title, content });
    } else {
      await createNote({ title, content });
    }

    setTitle("");
    setContent("");
    setEditingId(null);
    loadNotes();
  };

  const handleEdit = (note) => {
    setEditingId(note.id);
    setTitle(note.title);
    setContent(note.content);
  };

  const handleDelete = async (id) => {
    if (window.confirm("Delete this note?")) {
      await deleteNote(id);
      loadNotes();
    }
  };

  return (
    <div className="student-home">
      <section className="welcome-card">
        <h1>My Notes</h1>
        <p>Your personal notes</p>
      </section>

      <section className="dashboard-card">
        <h2>{editingId ? "Edit note" : "New note"}</h2>

        <input
          placeholder="Note title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />

        <textarea
          placeholder="Write your note..."
          value={content}
          onChange={(e) => setContent(e.target.value)}
        />

        <button onClick={handleSave}>
          {editingId ? "Update" : "Create"}
        </button>
      </section>

      <div className="dashboard-grid">
        {notes.map(note => (
          <section className="dashboard-card" key={note.id}>
            <h3>{note.title}</h3>
            <p className="note-content">{note.content}</p>

            <div className="note-actions">
              <button onClick={() => handleEdit(note)}>Edit</button>
              <button className="danger" onClick={() => handleDelete(note.id)}>
                Delete
              </button>
            </div>
          </section>
        ))}

        {notes.length === 0 && (
          <section className="dashboard-card">
            <p className="empty">No notes yet</p>
          </section>
        )}
      </div>
    </div>
  );
}
