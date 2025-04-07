using api_flash_deck.DTOs;
using api_flash_deck.Mappers;
using api_flash_deck.Models;
using api_flash_deck.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetByUserAsync([FromRoute] int userId)
    {
        var cards = await _service.GetCardsForUserAsync(userId);

        if (cards.Count() == 0)
        {
            return NotFound();
        }
        
        var resultDtoList = cards.Select(card => CardMapper.MapFlashCardToCardDto(card));
        
        return Ok(resultDtoList);
    }

    [HttpPost]
    [Route("/add")]
    public async Task<IActionResult> AddCardAsync([FromBody] AddCardDto cardDto)
    {
        var newCard = CardMapper.MapAddCardDtoToFlashCard(cardDto);
        
        var resultModel = await _service.AddCardAsync(newCard);

        if (resultModel == null)
        {
            return BadRequest("This card already exists in the deck or you have reached the max of 50 cards per user.");
        }
        
        var resultDto = CardMapper.MapFlashCardToCardDto(resultModel);
        
        return CreatedAtAction(nameof(AddCardAsync), resultDto);
    }

    [HttpDelete]
    [Route("/delete/card/{cardId}")]
    public async Task<IActionResult> DeleteCardAsync([FromRoute] Guid cardId)
    {
        var deletedCard = await _service.DeleteCardAsync(cardId);

        if (deletedCard == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete]
    [Route("delete/user/{userId}/deck/{deckId}")]
    public async Task<IActionResult> DeleteDeckAsync([FromRoute] int userId, int deckId)
    {
        await _service.DeleteDeckAsync(userId, deckId);
        
        return NoContent();
    }

    [HttpPut]
    [Route("/update/card")]
    public async Task<IActionResult> UpdateCardAsync([FromBody] FlashCard card)
    {
        var modelResult = await _service.UpdateCardAsync(card);
        
        if (modelResult == null)
        {
            return NotFound();
        }
        
        var result = CardMapper.MapFlashCardToCardDto(modelResult);
        
        return Ok(result);
    }
    
    [Route("error-development")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleErrorDevelopmentAsync([FromServices] IWebHostEnvironment env)
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