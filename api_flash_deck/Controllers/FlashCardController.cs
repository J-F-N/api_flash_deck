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

    [HttpGet("{id}")]
    public IActionResult GetByUser([FromRoute] int userId)
    {
        var cards = _service.GetCardsForUser(userId);

        if (cards == null)
        {
            return NotFound();
        }
        
        return Ok(cards);
    }
}