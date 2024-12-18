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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Book>().Property(b => b.Authors).HasConversion(
            authors => string.Join(",", authors),
            authorsString => authorsString.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
    }
}