namespace StudentHub.Api.Models.Groups
{
    public class GroupAdminDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<UserShortDto> Students { get; set; } = new();
        public List<UserShortDto> Teachers { get; set; } = new();
    }

    public class UserShortDto
    {
        public string Id { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
    }
}