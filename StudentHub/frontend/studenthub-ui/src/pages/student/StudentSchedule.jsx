import { useEffect, useState } from "react";
import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";

import { getSchedules } from "../../api/scheduleService";
import "./StudentSchedule.css";

export default function StudentSchedule() {
  const [events, setEvents] = useState([]);

  const loadData = async () => {
    const schedule = await getSchedules();

    setEvents(
      schedule.map(s => ({
        id: s.id,
        title: `${s.courseTitle}${s.lessonType ? " • " + s.lessonType : ""}`,
        start: s.startTime,
        end: s.endTime,
      }))
    );
  };

  useEffect(() => {
    loadData();
  }, []);

  return (
    <div className="schedule-page">
      {/* BLUE HEADER */}
      <div className="page-header">
        <h1>My Schedule</h1>
        <p>Your personal class timetable</p>
      </div>

      <div className="card">
        <FullCalendar
          plugins={[dayGridPlugin, timeGridPlugin]}
          initialView="timeGridWeek"
          headerToolbar={{
            left: "prev,next today",
            center: "title",
            right: "dayGridMonth,timeGridWeek,timeGridDay",
          }}
          events={events}
          height="auto"
          editable={false}
          selectable={false}
        />
      </div>
    </div>
  );
}
