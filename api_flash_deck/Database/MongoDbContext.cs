using api_flash_deck.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace api_flash_deck.Database;

public class MongoDbContext : DbContext
{
    public DbSet<FlashCard> FlashCards { get; init; }

    public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<FlashCard>().ToCollection("FlashCards")
            .HasKey(card => card.Id);
    }
}