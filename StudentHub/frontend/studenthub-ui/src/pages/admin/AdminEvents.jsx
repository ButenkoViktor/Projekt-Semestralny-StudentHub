import { useEffect, useState } from "react";
import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";
import interactionPlugin from "@fullcalendar/interaction";

import api from "../../api/axios";
import {
  getSchedule,
  createSchedule,
  deleteSchedule
} from "../../api/scheduleAdmin";

import "./AdminEvents.css";

export default function AdminSchedule() {
  const [events, setEvents] = useState([]);
  const [courses, setCourses] = useState([]);
  const [groups, setGroups] = useState([]);

  const [courseId, setCourseId] = useState("");
  const [groupId, setGroupId] = useState("");
  const [lessonType, setLessonType] = useState("");
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");

  const loadData = async () => {
    const [schedule, coursesRes, groupsRes] = await Promise.all([
      getSchedule(),
      api.get("/courses"),
      api.get("/admin/groups"),
    ]);

    setCourses(coursesRes.data);
    setGroups(groupsRes.data);

    setEvents(
      schedule.map(s => ({
        id: s.id,
        title: `${s.courseTitle} (${s.lessonType || "Lesson"})`,
        start: s.startTime,
        end: s.endTime,
      }))
    );
  };

  useEffect(() => {
    loadData();
  }, []);

  const handleCreate = async () => {
    if (!courseId || !startTime || !endTime) {
      alert("Course, start and end time are required");
      return;
    }

    await createSchedule({
      courseId: Number(courseId),
      teacherName: "Auto Teacher",
      startTime,
      endTime,
      groupId: groupId ? Number(groupId) : null,
      lessonType
    });

    alert("Lesson created");
    loadData();
  };

  const onEventClick = async (info) => {
    if (window.confirm("Delete this lesson?")) {
      await deleteSchedule(info.event.id);
      loadData();
    }
  };

  return (
    <div className="schedule-page">
      <h1>Admin → Schedule</h1>

      <div className="card">
        <h3>Create lesson</h3>

        <select value={courseId} onChange={e => setCourseId(e.target.value)}>
          <option value="">Select course</option>
          {courses.map(c => (
            <option key={c.id} value={c.id}>{c.title}</option>
          ))}
        </select>

        <select value={groupId} onChange={e => setGroupId(e.target.value)}>
          <option value="">All groups</option>
          {groups.map(g => (
            <option key={g.id} value={g.id}>{g.name}</option>
          ))}
        </select>

        <input
          placeholder="Lesson type (Lecture, Practice...)"
          value={lessonType}
          onChange={e => setLessonType(e.target.value)}
        />

        <input
          type="datetime-local"
          value={startTime}
          onChange={e => setStartTime(e.target.value)}
        />

        <input
          type="datetime-local"
          value={endTime}
          onChange={e => setEndTime(e.target.value)}
        />

        <button onClick={handleCreate}>Create</button>
      </div>

      <div className="card">
        <FullCalendar
          plugins={[dayGridPlugin, timeGridPlugin, interactionPlugin]}
          initialView="timeGridWeek"
          headerToolbar={{
            left: "prev,next today",
            center: "title",
            right: "dayGridMonth,timeGridWeek,timeGridDay"
          }}
          events={events}
          eventClick={onEventClick}
          height="auto"
        />
      </div>
    </div>
  );
}
