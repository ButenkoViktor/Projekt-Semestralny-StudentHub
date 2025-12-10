namespace StudentHub.Api.Models.Annoucements
{
    public class CreateAnnouncementRequest
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public int? GroupId { get; set; }
        public bool Published { get; set; } = true;
        public string? TargetGroup { get; set; }
    }
}
