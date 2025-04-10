using api_flash_deck.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api_flash_deck.Database;

public class MongoDbContext : IdentityDbContext<AppUser>
{
    public DbSet<FlashCard> FlashCards { get; init; }

    public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options)
    {
    }
}