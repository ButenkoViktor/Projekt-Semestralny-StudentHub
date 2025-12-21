namespace StudentHub.Api.Models.Notes
{
    public class CreateNoteDto
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
    }
}
