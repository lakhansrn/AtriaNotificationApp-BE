using System.Collections.Generic;
using System.Net.Mail;
using AtriaNotificationApp.Common.Interfaces;

namespace AtriaNotificationApp.Common.Services
{
    public class MailService : IMailService
    {

        private const string emailFrom ="atriakent990@gmail.com";
        private const string password ="kent990clark";
        public void SendMail(IEnumerable<string> to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
 
                mail.From = new MailAddress(emailFrom);

                foreach (var item in to)
                {
                    mail.To.Add(item);
                }
                
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml=true;
                //Attachment attachment = new Attachment(filename);
                //mail.Attachments.Add(attachment);
 
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, password);

                SmtpServer.Send(mail);
        }
    }
}