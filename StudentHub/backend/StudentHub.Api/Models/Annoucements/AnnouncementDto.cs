namespace StudentHub.Api.Models.Annoucements
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string AuthorId { get; set; } = default!;
        public string AuthorName { get; set; } = default!;
        public int? GroupId { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
