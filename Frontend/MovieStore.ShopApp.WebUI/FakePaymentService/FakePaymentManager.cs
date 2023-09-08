using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MovieStore.ShopApp.WebUI.FakePaymentService
{
    public class FakePaymentManager : IFakePaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FakePaymentManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        //----------------------------------------------------------------

        public async Task<bool> ReceivePayment(FakePaymentInfoInput fakePaymentInfoInput)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(fakePaymentInfoInput);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7292/FakePayments", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return false;
                
            }
            return true;
        }
    }
}
