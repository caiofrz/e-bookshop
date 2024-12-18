namespace backend_api.Infraestructure.Data;

using backend_api.Domain.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().Property(b => b.Authors).HasConversion(
           authors => string.Join(",", authors),
           authorsString => authorsString.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        modelBuilder.Entity<SaleItem>()
            .HasOne(si => si.Book)
            .WithMany()
            .HasForeignKey(si => si.BookId);
    }
}
