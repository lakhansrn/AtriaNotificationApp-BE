using System.Collections.Generic;

namespace AtriaNotificationApp.Common.Interfaces
{
    public interface IMailService
    {
        void SendMail(IEnumerable<string> to,string subject,string body);
    }
}