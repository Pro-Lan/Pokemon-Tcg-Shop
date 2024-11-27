namespace PokemonTcg.DTOs
{
    public class CardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime ListedDate { get; set; }
    }
} 