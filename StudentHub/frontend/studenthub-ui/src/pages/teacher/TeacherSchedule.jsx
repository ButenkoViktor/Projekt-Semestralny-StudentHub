import { useEffect, useState } from "react";
import { getMyCourses } from "../../api/coursesService";
import {
  getSchedules,
  createSchedule,
  deleteSchedule,
} from "../../api/scheduleService";

import "./TeacherSchedule.css";

export default function TeacherSchedule() {
  const [courses, setCourses] = useState([]);
  const [schedules, setSchedules] = useState([]);

  const [courseId, setCourseId] = useState("");
  const [title, setTitle] = useState("");
  const [lessonType, setLessonType] = useState("");
  const [date, setDate] = useState("");
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    const [coursesData, schedulesData] = await Promise.all([
      getMyCourses(),
      getSchedules(),
    ]);

    setCourses(coursesData);
    setSchedules(schedulesData);
  };

  const handleCreate = async () => {
    if (!courseId || !date || !startTime || !endTime) {
      alert("Fill all required fields");
      return;
    }

    const start = new Date(`${date}T${startTime}`);
    const end = new Date(`${date}T${endTime}`);

    await createSchedule({
      courseId: Number(courseId),
      startTime: start.toISOString(),
      endTime: end.toISOString(),
      lessonType,
      groupId: null,
      teacherName: "auto", 
    });

    setTitle("");
    setLessonType("");
    setDate("");
    setStartTime("");
    setEndTime("");

    loadData();
  };

  const handleDelete = async (id) => {
    if (!window.confirm("Delete lesson?")) return;
    await deleteSchedule(id);
    loadData();
  };

  return (
    
    <div className="teacher-schedule">
      <section className="welcome-card">
        <h1>My Teaching Groups</h1>
        <p>
          Create and manage your teaching schedule. Add lessons and keep track of your classes.
        </p>
      </section>

      <div className="schedule-form">
        <select value={courseId} onChange={(e) => setCourseId(e.target.value)}>
          <option value="">Select course</option>
          {courses.map((c) => (
            <option key={c.id} value={c.id}>
              {c.title}
            </option>
          ))}
        </select>

        <input
          type="text"
          placeholder="Lesson title"
          value={lessonType}
          onChange={(e) => setLessonType(e.target.value)}
        />

        <input type="date" value={date} onChange={(e) => setDate(e.target.value)} />

        <div className="time-row">
          <input
            type="time"
            value={startTime}
            onChange={(e) => setStartTime(e.target.value)}
          />
          <input
            type="time"
            value={endTime}
            onChange={(e) => setEndTime(e.target.value)}
          />
        </div>

        <button onClick={handleCreate}>Create lesson</button>
      </div>

      <div className="schedule-list">
        {schedules.map((s) => (
          <div className="schedule-card" key={s.id}>
            <div>
              <strong>{s.courseTitle}</strong>
              <p>{s.lessonType}</p>
              <p>
                {new Date(s.startTime).toLocaleString()} –{" "}
                {new Date(s.endTime).toLocaleTimeString()}
              </p>
            </div>

            <button className="buttonD" onClick={() => handleDelete(s.id)}>✖</button>
          </div>
        ))}
      </div>
    </div>
  );
}
