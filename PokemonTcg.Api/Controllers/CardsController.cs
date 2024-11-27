using Microsoft.AspNetCore.Mvc;
using PokemonTcg.Services;
using PokemonTcg.DTOs;

namespace PokemonTcg.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CardDto>> GetCards(
            [FromQuery] string? search = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null)
        {
            var cards = _cardService.GetCards(search, minPrice, maxPrice);
            return Ok(cards);
        }
    }
} 