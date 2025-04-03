using api_flash_deck.DTOs;
using api_flash_deck.Mappers;
using api_flash_deck.Models;
using api_flash_deck.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace api_flash_deck.Controllers;

[ApiController]
[Route("/cards")]
public class FlashCardController : ControllerBase
{
    private readonly IFlashCardService  _service;

    public FlashCardController(IFlashCardService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/user/{userId}")]
    public IActionResult GetByUser([FromRoute] int userId)
    {
        var cards = _service.GetCardsForUser(userId);

        if (cards == null)
        {
            return NotFound();
        }
        
        return Ok(cards);
    }

    [HttpPost]
    [Route("/add")]
    public IActionResult AddCard([FromBody] AddCardDto cardDto)
    {
        var newCard = CardMapper.MapAddCardDtoToFlashCard(cardDto);
        
        var result = _service.AddCard(newCard);
        
        return Ok(result);
    }

    [HttpDelete]
    [Route("/delete/user/card")]
    public IActionResult DeleteCard([FromBody] FlashCard card)
    {
        var result = _service.DeleteCard(card);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete]
    [Route("delete/user/{userId}/deck/{deckId}")]
    public IActionResult DeleteDeck([FromRoute] int userId, int deckId)
    {
        _service.DeleteDeck(userId, deckId);
        
        return NoContent();
    }

    [HttpPut]
    [Route("/update/card")]
    public FlashCard? UpdateCard([FromBody] FlashCard card)
    {
        var result = _service.UpdateCard(card);

        if (result == null)
        {
            NotFound();
        }
        
        return result;
    }
    
    [Route("error-development")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleErrorDevelopment([FromServices] IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            return NotFound();
        }
        
        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        
        return Problem(detail: exceptionHandlerFeature.Error.StackTrace, title: exceptionHandlerFeature.Error.Message);
    }

    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleError() => Problem();
}