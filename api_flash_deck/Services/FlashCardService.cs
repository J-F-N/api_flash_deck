using api_flash_deck.Database;
using api_flash_deck.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SharpCompress.Common;

namespace api_flash_deck.Services;

public class FlashCardService : IFlashCardService
{
    private readonly ILogger<FlashCardService> _logger;
    private readonly MongoDbContext _dbContext;


    public FlashCardService(ILogger<FlashCardService> logger, MongoDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public List<FlashCard>? GetCardsForUser(int userId)
    {
        var cards = _dbContext.FlashCards.Where(card => card.UserId == userId).AsNoTracking().ToList();

        if (!cards.Any())
        {
            _logger.LogInformation($"No cards found for user \"{userId}\"");
        }
        
        return cards;
    }

    public FlashCard? AddCard(FlashCard card)
    {
        var matchingEntry = _dbContext.FlashCards.FirstOrDefault(
            entry => entry.UserId == card.UserId && 
                     entry.DeckId == card.DeckId &&
                     entry.Prompt == card.Prompt &&
                     entry.Answer == card.Answer);

        if (matchingEntry != null)
        {
            _logger.LogInformation($"Cannot Add: Card \"{matchingEntry.Prompt}\" is already in the database for this deck and user.");
            return null;
        }
        
        card.Id = Guid.NewGuid();
        _dbContext.FlashCards.Add(card);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        _dbContext.SaveChanges();

        return card;
    }

    public FlashCard? DeleteCard(FlashCard card)
    {
        var deletedCard = _dbContext.FlashCards.FirstOrDefault(entry => entry.Id == card.Id);
        
        if (deletedCard == null)
        {
            _logger.LogInformation($"Cannot Delete: No such card ID \"{card.Id}\", found in database.");
            return null;
        }

        _dbContext.FlashCards.Remove(card);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        _dbContext.SaveChanges();
        
        return deletedCard;
    }

    public FlashCard? UpdateCard(FlashCard card)
    {
        var entry = _dbContext.FlashCards.FirstOrDefault(entry => entry.Id == card.Id);

        if (entry == null)
        {
            _logger.LogInformation($"Cannot Update: No card found with id \"{card.Id}\" for user {card.UserId}");
            return null;
        }
        var entity = _dbContext.FlashCards.Update(card);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        _dbContext.SaveChanges();
        
        return entry;
    }

    public void DeleteDeck(int userId, int deckId)
    {
        _dbContext.RemoveRange(_dbContext.FlashCards.Where(card => card.UserId == userId &&
                                                                   card.DeckId == deckId));
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        _dbContext.SaveChanges();
    }
}