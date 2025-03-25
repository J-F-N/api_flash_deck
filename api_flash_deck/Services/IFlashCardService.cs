using api_flash_deck.Models;

namespace api_flash_deck.Services;

public interface IFlashCardService
{
    IEnumerable<FlashCard> GetCardsForUser(int userId);
    void AddCard(FlashCard card);
    void DeleteCard(FlashCard card);
    void UpdateCard(FlashCard card);
}