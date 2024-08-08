namespace CodeFandango.Flamenco.Data
{
    public class Media
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string MediaHandler { get; set; } 
        public required string OriginalFilename { get; set; } 
        public required string MimeType { get; set; }
        public bool IsImage { get; set; }
        public ICollection<SizedImage> Sizes { get; set; } = new HashSet<SizedImage>();
    }
}