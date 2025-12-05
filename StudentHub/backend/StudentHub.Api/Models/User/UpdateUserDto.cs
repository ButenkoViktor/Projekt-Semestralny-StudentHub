namespace StudentHub.Api.Models.User
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? AvatarUrl { get; set; }

        public int? GroupId { get; set; }
    }
}
