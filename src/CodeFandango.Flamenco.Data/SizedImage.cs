namespace CodeFandango.Flamenco.Data
{
    public class SizedImage
    {
        public long Id { get; set; }
        public long MediaId { get; set; }
        public required Media Media { get; set; }
        public required ImageSize Size { get; set; }
        public required string Path { get; set; }
    }
}