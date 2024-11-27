namespace PokemonTcg.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public List<CardDto> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
} 