using PokemonTcg.DTOs;

namespace PokemonTcg.Services
{
    public interface ICardService
    {
        IEnumerable<CardDto> GetCards(string searchQuery = null, decimal? minPrice = null, decimal? maxPrice = null);
    }
} 