namespace StudentHub.Api.Models.Roles
{
    public class UserWithRolesDto
    {
        public string Id { get; set; } = default!;
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}