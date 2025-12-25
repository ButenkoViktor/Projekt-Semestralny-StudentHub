namespace StudentHub.Api.Models.Chat
{
    public class UserSearchDto
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public bool IsOnline { get; set; }
    }
}
