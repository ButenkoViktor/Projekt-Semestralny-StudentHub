public class CourseWithTeacherDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    public string TeacherFirstName { get; set; } = default!;
    public string TeacherLastName { get; set; } = default!;
    public string TeacherEmail { get; set; } = default!;
}
