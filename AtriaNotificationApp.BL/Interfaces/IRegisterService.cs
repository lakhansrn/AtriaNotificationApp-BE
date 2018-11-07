using AtriaNotificationApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.BL.Services
{
    public interface IRegisterService
    {
        Task RegisterAnnouncerAsync(string email, string role);

        Task<Register> GetRegisterAnnouncerAsync(Guid guid);
        Task RegisterContentWriterAsync(string email, string v, Guid announcerGuid);

        // Task<Register> CompleteRegisterAnnouncerAsync(User user);
    }
}
