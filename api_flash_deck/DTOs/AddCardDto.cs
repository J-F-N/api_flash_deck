namespace api_flash_deck.DTOs;

public class AddCardDto
{
    public int UserId { get; set; }
    public int DeckId { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}