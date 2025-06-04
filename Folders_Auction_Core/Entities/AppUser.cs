namespace Folders_Auction_Core.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        // Eski: public ICollection<UserFile>? Files { get; set; }
        public ICollection<FileEntity>? Files { get; set; }

    }
}
