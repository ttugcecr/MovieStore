using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.BusinessLayer.Abstract;

namespace MovieStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult ListOrder()
        {
            var values = _orderService.GetAll();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var values = _orderService.GetById(id);
            return Ok(values);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var value = _orderService.GetById(id);
            _orderService.Delete(value);
            return Ok();
        }
    }
}
