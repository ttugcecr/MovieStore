namespace MovieStore.ShopApp.WebUI.FakePaymentService
{
    public interface IFakePaymentService
    {
        Task<bool> ReceivePayment(FakePaymentInfoInput fakePaymentInfoInput);
    }
}
