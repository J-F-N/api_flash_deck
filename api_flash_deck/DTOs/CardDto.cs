namespace api_flash_deck.DTOs;

public class CardDto
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public int DeckId { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}