using System;
using System.Collections.Generic;
using System.Text;
using AtriaNotificationApp.DAL.Interfaces;
using AtriaNotificationApp.DAL.Repositories;
using AtriaNotificationApp.DAL.Entities;
using System.Threading.Tasks;
using AtriaNotificationApp.Common.Interfaces;
using AtriaNotificationApp.Common.Services;

namespace AtriaNotificationApp.BL.Services
{
    public class RegisterService: IRegisterService
    {
        private readonly IRegisterAggregateRepo registerAggregateRoot;
        private readonly IMailService mailService;
        private const string mailSubject = "Invitation for Atria Notification App";
        private const string prodEndPoint = "https://atrianotifications.azurewebsites.net/announcerRegistration?id=";
        private const string localEndPoint = "http://localhost:4200/announcerRegistration?id=";

        public RegisterService()
        {
            registerAggregateRoot = new RegisterAggregateRepo();
            mailService = new MailService();
        }

        public async Task RegisterAnnouncerAsync(string email, string role)
        {
            var registerDetails = new Register() { Email = email, Role = role };
            var registerModel = await registerAggregateRoot.RegisterUser(registerDetails);
            mailService.SendMail(new List<String>() { email }, mailSubject, GenerateMailBody(role, registerModel.Id.ToString()));
        }

        public async Task RegisterContentWriterAsync(string email, string role, Guid announcerGuid)
        {
            var registerDetails = new Register() { Email = email, Role = role, ReportsTo = announcerGuid };
            var registerModel = await registerAggregateRoot.RegisterUser(registerDetails);
            mailService.SendMail(new List<String>() { email }, mailSubject, GenerateMailBody(role, registerModel.Id.ToString()));
        }

        public async Task<Register> GetRegisterUserAsync(Guid guid)
        {
            return await registerAggregateRoot.GetRegisterAnnouncer(guid);
        }

        //public Task<Register> CompleteRegisterAnnouncerAsync(User user)
        //{
        //    return ;
        //}

        private string GenerateMailBody(string role, string guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<body>Hi, <br/> ");
            builder.Append($"It is our pleasure to state that you have been referred for being {role} of the new Notification App of Atria. Please follow the below link to complete the registration process.<br/><br/> ");
            builder.Append($"{localEndPoint}{guid} <br /><br/>For any support related to registration please reply back to the mail.<br/><br/> ");
            builder.Append("Regards, <br/>Notification Support Team <br/>CS Department <br/>Atria</body> ");

            return builder.ToString();
        }
    }
}
