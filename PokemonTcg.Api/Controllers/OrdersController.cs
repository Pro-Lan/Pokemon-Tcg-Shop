using Microsoft.AspNetCore.Mvc;
using PokemonTcg.DTOs;

namespace PokemonTcg.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static List<OrderDto> _orders = new List<OrderDto>();
        private static int _nextOrderId = 1;

        [HttpPost]
        public ActionResult<OrderDto> CreateOrder([FromBody] OrderDto order)
        {
            order.OrderId = _nextOrderId++;
            order.OrderDate = DateTime.Now;
            _orders.Add(order);
            return Ok(order);
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetOrders()
        {
            return Ok(_orders);
        }
    }
} 