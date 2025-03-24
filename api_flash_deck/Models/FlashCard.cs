using MongoDB.Bson;

namespace api_flash_deck.Models;

public class FlashCard
{
    public ObjectId Id { get; set; }
    public string UserId { get; set; }
    public string Prompt { get; set; } 
    public string Answer { get; set; }
}