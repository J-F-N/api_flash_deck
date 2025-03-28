using api_flash_deck.Database;
using api_flash_deck.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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

    public FlashCard? DeleteCard(FlashCard card)
    {
        var deletedCard = _dbContext.FlashCards.FirstOrDefault(entity => entity.Id == card.Id);

        if (deletedCard == null)
        {
            return null;
        }
        
        _dbContext.FlashCards.Remove(deletedCard);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        _dbContext.SaveChanges();
        
        return deletedCard;
    }

    public void UpdateCard(FlashCard card)
    {
        _dbContext.FlashCards.Update(card);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        _dbContext.SaveChanges();
    }

    public void DeleteDeck(List<FlashCard> cardList)
    {
        _dbContext.RemoveRange(cardList);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        _dbContext.SaveChanges();
    }
}