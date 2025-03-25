using api_flash_deck.Database;
using api_flash_deck.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace api_flash_deck.Services;

public class FlashCardService : IFlashCardService
{
    private readonly ILogger<FlashCardService> _logger;
    private readonly MongoDbContext _dbContext;
    
    public IEnumerable<FlashCard> GetCardsForUser(int userId)
    {
        return _dbContext.FlashCards.Where(f => f.UserId == userId).AsNoTracking().ToList();
    }

    public void AddCard(FlashCard card)
    {
        _dbContext.FlashCards.Add(card);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        _dbContext.SaveChanges();
    }

    public void DeleteCard(FlashCard card)
    {
        _dbContext.FlashCards.Remove(card);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        _dbContext.SaveChanges();
    }

    public void UpdateCard(FlashCard card)
    {
        _dbContext.FlashCards.Update(card);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        _dbContext.SaveChanges();
    }
}