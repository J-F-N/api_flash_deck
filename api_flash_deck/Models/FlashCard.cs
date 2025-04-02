using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace api_flash_deck.Models;

[Collection("FlashCards")]
public class FlashCard
{
    public ObjectId Id { get; protected set; }
    public int UserId { get; set; }
    public int DeckId { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}