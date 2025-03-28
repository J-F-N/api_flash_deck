using api_flash_deck.Models;

namespace api_flash_deck.Services;

public interface IFlashCardService
{
    IEnumerable<FlashCard> GetCardsForUser(int userId);
    void AddCard(FlashCard card);
    FlashCard? DeleteCard(FlashCard card);
    void UpdateCard(FlashCard card);
}