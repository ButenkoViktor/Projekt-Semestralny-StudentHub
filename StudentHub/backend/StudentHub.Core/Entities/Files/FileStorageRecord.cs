public class FileStorageRecord
{
    public int Id { get; set; }
    public string FilePath { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public string? UploadedBy { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}