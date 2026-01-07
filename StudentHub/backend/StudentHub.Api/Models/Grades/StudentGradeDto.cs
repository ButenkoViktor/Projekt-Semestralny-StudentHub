public class StudentGradeDto
{
    public string StudentId { get; set; } = default!;
    public string StudentName { get; set; } = default!;
    public DateTime? Date { get; set; }
    public bool IsPresent { get; set; }
    public int? Grade { get; set; }
}