using api_flash_deck.Models;

namespace api_flash_deck.Services;

public interface IFlashCardService
{
    Task<List<FlashCard>> GetCardsForUserAsync(int userId);
    Task<FlashCard?> AddCardAsync(FlashCard card);
    Task<FlashCard?> DeleteCardAsync(Guid cardId);
    Task<FlashCard?> UpdateCardAsync(FlashCard card);
    Task DeleteDeckAsync(int userId, int deckId);
}