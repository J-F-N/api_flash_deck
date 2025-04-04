using api_flash_deck.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using MongoDB.EntityFrameworkCore.Extensions;

namespace api_flash_deck.Database;

public class MongoDbContext : DbContext
{
    public DbSet<FlashCard> FlashCards { get; init; }

    public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options)
    {
    }
}