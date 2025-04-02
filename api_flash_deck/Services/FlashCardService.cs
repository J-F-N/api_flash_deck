using api_flash_deck.Database;
using api_flash_deck.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace api_flash_deck.Services;

public class FlashCardService : IFlashCardService
{
    private readonly ILogger<FlashCardService> _logger;
    private readonly MongoDbContext _dbContext;
    
    
    
    public List<FlashCard>? GetCardsForUser(int userId)
    {
        var cards = _dbContext.FlashCards.Where(f => f.UserId == userId).AsNoTracking().ToList();

        if (cards == null)
        {
            _logger.LogInformation($"No cards found for user {userId}");
        }
        
        return cards;
    }

    public FlashCard? AddCard(FlashCard card)
    {
        var matchingEntries = _dbContext.FlashCards.Where(
            entry => entry.UserId == card.UserId &&
                     entry.Prompt == card.Prompt &&
                     entry.Answer == card.Answer).ToList();

        if (matchingEntries.Count() > 0)
        {
            return null;
        }
        
        _dbContext.FlashCards.Add(card);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        _dbContext.SaveChanges();

        return card;
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

    public FlashCard? UpdateCard(FlashCard card)
    {
        var updatedCard = _dbContext.FlashCards.FirstOrDefault(entity => entity.Id == card.Id);

        if (updatedCard == null)
        {
            return null;
        }
        
        _dbContext.FlashCards.Update(card);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        _dbContext.SaveChanges();
        _dbContext.Entry(updatedCard).Reload();
        
        return updatedCard;
    }

    public void DeleteDeck(List<FlashCard> cardList)
    {
        _dbContext.RemoveRange(cardList);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        _dbContext.SaveChanges();
    }
}