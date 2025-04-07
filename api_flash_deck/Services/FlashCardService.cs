using api_flash_deck.Database;
using api_flash_deck.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<FlashCard>> GetCardsForUserAsync(int userId)
    {
        var cards = await _dbContext.FlashCards.Where(card => card.UserId == userId).AsNoTracking().ToListAsync();

        if (cards.Count() == 0)
        {
            _logger.LogInformation($"No cards found for user \"{userId}\"");
        }
        
        return cards;
    }

    public async Task<FlashCard?> AddCardAsync(FlashCard card)
    {
        var userCards = await GetCardsForUserAsync(card.UserId);

        if (userCards.Count == 50)
        {
            _logger.LogInformation($"Card not added, user \"{card.UserId}\" at max 50 cards.");
            return null;
        }

        var matchingEntry = userCards.FirstOrDefault(entry => entry.DeckId == card.DeckId &&
                                          entry.Prompt == card.Prompt &&
                                          entry.Answer == card.Answer);

        if (matchingEntry != null)
        {
            _logger.LogInformation($"Cannot Add: Card \"{matchingEntry.Prompt}\" is already in the database for this deck and user.");
            return null;
        }
        
        await _dbContext.FlashCards.AddAsync(card);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        await _dbContext.SaveChangesAsync();

        return card;
    }

    public async Task<FlashCard?> DeleteCardAsync(Guid cardId)
    {
        var deletedCard = await _dbContext.FlashCards.FirstOrDefaultAsync(entry => entry.Id == cardId);
        
        if (deletedCard == null)
        {
            _logger.LogInformation($"Cannot Delete: No such card ID \"{cardId}\", found in database.");
            return null;
        }

        _dbContext.FlashCards.Remove(deletedCard);
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        await _dbContext.SaveChangesAsync();
        
        return deletedCard;
    }

    public async Task<FlashCard?> UpdateCardAsync(FlashCard card)
    {
        var entry = await _dbContext.FlashCards.FirstOrDefaultAsync(entry => entry.Id == card.Id);

        if (entry == null)
        {
            _logger.LogInformation($"Cannot Update: No card found with id \"{card.Id}\" for user {card.UserId}");
            return null;
        }
        entry.DeckId = card.DeckId;
        entry.Prompt = card.Prompt;
        entry.Answer = card.Answer;
        
        _dbContext.ChangeTracker.DetectChanges();
        _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
        await _dbContext.SaveChangesAsync();
        
        return entry;
    }

    public async Task DeleteDeckAsync(int userId, int deckId)
    {
        var deckEntries = _dbContext.FlashCards.Where(card => card.UserId == userId &&
                                                              card.DeckId == deckId).ToListAsync();

        if (deckEntries.Result.Count() == 0)
        {
            _logger.LogInformation($"No cards found for user \"{userId}\" with deck ID \"{deckId}\"");
        }
        else
        {
            _dbContext.RemoveRange(deckEntries);
            _dbContext.ChangeTracker.DetectChanges();
            _logger.LogInformation(_dbContext.ChangeTracker.DebugView.LongView);
            await _dbContext.SaveChangesAsync();
        }
    }
}