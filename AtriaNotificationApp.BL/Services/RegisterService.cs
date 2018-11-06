using System;
using System.Collections.Generic;
using System.Text;
using AtriaNotificationApp.DAL.Interfaces;
using AtriaNotificationApp.DAL.Repositories;
using AtriaNotificationApp.DAL.Entities;
using System.Threading.Tasks;

namespace AtriaNotificationApp.BL.Services
{
    public class RegisterService: IRegisterService
    {
        private readonly IRegisterAggregateRoot registerAggregateRoot;
      
        public RegisterService()
        {
            registerAggregateRoot = new RegisterAggregateRoot();
        }

        public async Task<Register> RegisterAnnouncerAsync(string email)
        {
            var registerDetails = new Register() { Email = email, Role = "announcer" };
            return await registerAggregateRoot.RegisterAnnouncer(email, registerDetails);
        }

        public async Task<Register> GetRegisterAnnouncerAsync(Guid guid)
        {
            return await registerAggregateRoot.GetRegisterAnnouncer(guid);
        }

        //public Task<Register> CompleteRegisterAnnouncerAsync(User user)
        //{
        //    return ;
        //}
    }
}
