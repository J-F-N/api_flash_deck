using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace api_flash_deck.Models;

[Collection("FlashCards")]
public class FlashCard
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int DeckId { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}