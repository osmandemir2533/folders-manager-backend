namespace Folders_Auction_Core.Entities
{
    public class FileEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public DateTime UploadDate { get; set; }

        // Kullanıcı ile ilişki
        public int UploadedByUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
    }
}
