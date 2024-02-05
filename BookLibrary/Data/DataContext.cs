using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Book> books { get; set; }
        public DbSet<BookAuthor> bookAuthors { get; set; }
        public DbSet<BookCategory> bookCategories { get; set; }
        public DbSet<Member> members { get; set; }
        public DbSet<RentedBook> rentedBooks { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RentedBook>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<RentedBook>()
                .HasOne(p => p.book)
                .WithMany(pc => pc.RentedBooks)
                .HasForeignKey(p => p.bookId);
            modelBuilder.Entity<RentedBook>()
                .HasOne(p => p.member)
                .WithMany(pc => pc.RentedBooks)
                .HasForeignKey(p => p.memberId);
        }

    }
}
