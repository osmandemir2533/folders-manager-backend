using Microsoft.EntityFrameworkCore;
using Folders_Auction_Core.Entities;

namespace Folders_Auction_Data_Access.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<AppUser> AppUsers => Set<AppUser>();
        public DbSet<FileEntity> Files => Set<FileEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // FileEntity ile AppUser arasında ilişki kur
            modelBuilder.Entity<FileEntity>()
              .HasOne(f => f.AppUser)
              .WithMany(u => u.Files)
              .HasForeignKey(f => f.UploadedByUserId);

        }
    }
}
