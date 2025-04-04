using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace api_flash_deck.Models;

[Collection("FlashCards")]
public class FlashCard
{
    [Key]
    public Guid Id { get; set; }
    ObjectId Identity { get; set; }
    public int UserId { get; set; }
    public int DeckId { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}