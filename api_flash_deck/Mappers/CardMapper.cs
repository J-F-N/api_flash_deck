using api_flash_deck.DTOs;
using api_flash_deck.Models;

namespace api_flash_deck.Mappers;

public static class CardMapper
{
    public static FlashCard MapAddCardDtoToFlashCard(AddCardDto cardDto)
    {
        return new FlashCard
        {
            UserId = cardDto.UserId,
            DeckId = cardDto.DeckId,
            Prompt = cardDto.Prompt,
            Answer = cardDto.Answer,
        };
    }

    public static CardDto MapFlashCardToCardDto(FlashCard flashCard)
    {
        return new CardDto
        {
            Id = flashCard.Id,
            UserId = flashCard.UserId,
            DeckId = flashCard.DeckId,
            Prompt = flashCard.Prompt,
            Answer = flashCard.Answer,
        };
    }
}