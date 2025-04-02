using api_flash_deck.Models;
using MongoDB.Bson;

namespace api_flash_deck.Services;

public interface IFlashCardService
{
    List<FlashCard>? GetCardsForUser(int userId);
    FlashCard? AddCard(FlashCard card);
    FlashCard? DeleteCard(FlashCard card);
    FlashCard? UpdateCard(FlashCard card);
    void DeleteDeck(int userId, int deckId);
}