namespace MovieStore.ShopApp.WebUI.SendMail
{
    public interface ISendEMail
    {
        void SendMailForRegister(string Name,string Surname,string emailAdress,string url);//hesabı onayla
        void SendMailForForgotPassword(string emailAdress,string url);//şifremı unuttum
    }
}
