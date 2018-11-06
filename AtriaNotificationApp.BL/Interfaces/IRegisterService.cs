using AtriaNotificationApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.BL.Services
{
    public interface IRegisterService
    {
        Task<Register> RegisterAnnouncerAsync(string email);

        Task<Register> GetRegisterAnnouncerAsync(Guid guid);

        // Task<Register> CompleteRegisterAnnouncerAsync(User user);
    }
}
