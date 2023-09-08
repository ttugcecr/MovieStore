using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.FakePayment.Dtos;

namespace MovieStore.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult ReceivePayment(FakePaymentDto fakePaymentDto) // secron için kullanmıştık
        {
           if (fakePaymentDto != null)
            {
                return Ok(200);
            }
            
                return Ok(204);
            

        }
    }
}
