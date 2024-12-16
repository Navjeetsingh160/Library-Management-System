using Library_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<AdminLibrary> AdminLibraries { get; set; }

        public DbSet<TeacherLogin> Teachers { get; set; }

   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Transaction -> Book (Many-to-One)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Book)
                .WithMany(b => b.Transactions)
                .HasForeignKey(t => t.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // Transaction -> User (Many-to-One)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
