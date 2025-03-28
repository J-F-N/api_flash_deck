using api_flash_deck.Models;
using api_flash_deck.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_flash_deck.Controllers;

[ApiController]
[Route("/cards")]
public class FlashCardController : ControllerBase
{
    private readonly IFlashCardService  _service;
    private readonly ILogger<FlashCardController> _logger;

    public FlashCardController(IFlashCardService service, ILogger<FlashCardController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [Route("/user/{id}")]
    public IActionResult GetByUser([FromRoute] int userId)
    {
        var cards = _service.GetCardsForUser(userId);

        if (cards == null)
        {
            _logger.LogInformation($"No cards found for {userId}");
            return NotFound();
        }
        
        return Ok(cards);
    }

    [HttpPost]
    [Route("/add")]
    public IActionResult AddCard([FromBody] FlashCard card)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public IActionResult DeleteCard([FromBody] FlashCard card)
    {
        var result = _service.DeleteCard(card);

        if (result == null)
        {
            NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Route("delete/deck/{id}")]
    public IActionResult DeleteDeck([FromRoute] int deckId)
    {
        throw new NotImplementedException();
    }
}