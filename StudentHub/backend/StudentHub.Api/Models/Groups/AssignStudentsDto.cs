namespace StudentHub.Api.Models.Groups
{
    public class AssignStudentsDto
    {
        public int GroupId { get; set; }
        public List<string> StudentIds { get; set; } = new();
    }
}