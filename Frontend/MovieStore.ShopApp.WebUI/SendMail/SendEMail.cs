using MimeKit;
namespace MovieStore.ShopApp.WebUI.SendMail
{
    public class SendEMail : ISendEMail
    {
        public void SendMailForForgotPassword(string emailAdress, string url)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Movie Project admin", "yazilimsincom@gmail.com");
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", emailAdress);
            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            //bodyBuilder.TextBody = "Kayıt İşlemi gerçekleştirmek için onay kodunuz:" + confirmCode;

            bodyBuilder.TextBody = $"Lütfen Şifrenizi değiştirmek için  linke <a href='https://localhost:7292/{url}'>tıklayınız.</a>";

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = emailAdress + "Şifre Sıfırlama";


            MailKit.Net.Smtp.SmtpClient smtpClient = new MailKit.Net.Smtp.SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("yazilimsincom@gmail.com", "yjekytqznuunyxjy");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);
        }

        public void SendMailForRegister(string Name, string Surname, string emailAdress,string url)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Movie Project admin", "yazilimsincom@gmail.com");
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", emailAdress);
            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            //bodyBuilder.TextBody = "Kayıt İşlemi gerçekleştirmek için onay kodunuz:" + confirmCode;

            bodyBuilder.TextBody = $"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:7292{url}'>tıklayınız.</a>";

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = Name + Surname + "  " + " Hesabınızı onaylayınız. ";

            MailKit.Net.Smtp.SmtpClient smtpClient = new MailKit.Net.Smtp.SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("yazilimsincom@gmail.com", "yjekytqznuunyxjy");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);

        }
    }
}
