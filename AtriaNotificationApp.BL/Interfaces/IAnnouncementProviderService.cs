using AtriaNotificationApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.BL.Interfaces
{
    public interface IAnnouncementProviderService
    {
        Task<IEnumerable<Content>> GetContentsAsync(Guid id);
    }
}
