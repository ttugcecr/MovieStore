namespace MovieStore.ShopApp.WebUI.FakePaymentService
{
    public class FakePaymentInfoInput
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }//karttta 2 hanelı tarıh ay 
        public string ExpirationYear { get; set; }//karttta 2 hanelı tarıh  yıl
        public string CVV { get; set; }//arka yuzdkeı 3 3 hane
        public double TotalPrice { get; set; }//sepettekı toplam fıyat
    }
}
