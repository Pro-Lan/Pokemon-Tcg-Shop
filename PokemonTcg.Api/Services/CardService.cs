using PokemonTcg.DTOs;

namespace PokemonTcg.Services
{
    public class CardService : ICardService
    {
        public IEnumerable<CardDto> GetCards(string searchQuery = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var cards = new List<CardDto>
            {
                new CardDto 
                { 
                    Id = 1, 
                    Name = "皮卡丘 V",
                    ImageUrl = "https://images.pokemontcg.io/swsh4/44.png",
                    Price = 300,
                    IsAvailable = true,
                    ListedDate = new DateTime(2024, 7, 5)
                },
                new CardDto 
                { 
                    Id = 2, 
                    Name = "超夢 VMAX",
                    ImageUrl = "https://images.pokemontcg.io/swsh3/67.png",
                    Price = 600,
                    IsAvailable = true,
                    ListedDate = new DateTime(2024, 7, 10)
                },
                new CardDto 
                { 
                    Id = 3, 
                    Name = "噴火龍 GX",
                    ImageUrl = "https://images.pokemontcg.io/sm12/20.png",
                    Price = 900,
                    IsAvailable = false,
                    ListedDate = new DateTime(2024, 7, 15)
                },
                new CardDto 
                { 
                    Id = 4, 
                    Name = "快龍 V",
                    ImageUrl = "https://images.pokemontcg.io/swsh7/147.png",
                    Price = 450,
                    IsAvailable = true,
                    ListedDate = new DateTime(2024, 7, 20)
                },
                new CardDto 
                { 
                    Id = 5, 
                    Name = "妙蛙花 VMAX",
                    ImageUrl = "https://images.pokemontcg.io/swsh2/1.png",
                    Price = 400,
                    IsAvailable = true,
                    ListedDate = new DateTime(2024, 7, 25)
                },
                new CardDto 
                { 
                    Id = 6, 
                    Name = "水箭龜 V",
                    ImageUrl = "https://images.pokemontcg.io/swsh1/14.png",
                    Price = 350,
                    IsAvailable = true,
                    ListedDate = new DateTime(2024, 7, 30)
                },
                new CardDto 
                { 
                    Id = 7, 
                    Name = "傑尼龜 特別版",
                    ImageUrl = "https://images.pokemontcg.io/swsh1/13.png",
                    Price = 200,
                    IsAvailable = true,
                    ListedDate = new DateTime(2024, 7, 8)
                },
                new CardDto 
                { 
                    Id = 8, 
                    Name = "卡比獸 VMAX",
                    ImageUrl = "https://images.pokemontcg.io/swsh1/142.png",
                    Price = 500,
                    IsAvailable = false,
                    ListedDate = new DateTime(2024, 7, 12)
                },
                new CardDto 
                { 
                    Id = 9, 
                    Name = "夢幻 V",
                    ImageUrl = "https://images.pokemontcg.io/swsh5/134.png",
                    Price = 800,
                    IsAvailable = true,
                    ListedDate = new DateTime(2024, 7, 18)
                },
                new CardDto 
                { 
                    Id = 10, 
                    Name = "路卡利歐 V",
                    ImageUrl = "https://images.pokemontcg.io/swsh3/135.png",
                    Price = 400,
                    IsAvailable = true,
                    ListedDate = new DateTime(2024, 7, 22)
                },
                new CardDto 
                { 
                    Id = 11, 
                    Name = "班基拉斯 V",
                    ImageUrl = "https://images.pokemontcg.io/swsh5/97.png",
                    Price = 450,
                    IsAvailable = true,
                    ListedDate = new DateTime(2024, 7, 28)
                },
                new CardDto 
                { 
                    Id = 12, 
                    Name = "甲賀忍蛙 GX",
                    ImageUrl = "https://images.pokemontcg.io/sm10/95.png",
                    Price = 600,
                    IsAvailable = false,
                    ListedDate = new DateTime(2024, 7, 3)
                }
            };

            var filteredCards = cards.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredCards = filteredCards.Where(card => 
                    card.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            if (minPrice.HasValue)
            {
                filteredCards = filteredCards.Where(card => card.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                filteredCards = filteredCards.Where(card => card.Price <= maxPrice.Value);
            }

            return filteredCards;
        }
    }
} 