using api_flash_deck.Models;

namespace api_flash_deck.Services;

public interface IFlashCardService
{
    List<FlashCard>? GetCardsForUser(int userId);
    FlashCard? AddCard(FlashCard card);
    FlashCard? DeleteCard(FlashCard card);
    FlashCard? UpdateCard(FlashCard card);
}