using AtriaNotificationApp.BL.Interfaces;
using AtriaNotificationApp.DAL.Entities;
using AtriaNotificationApp.DAL.Interfaces;
using AtriaNotificationApp.DAL.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.BL.Services
{
    public class AnnouncementProviderService: IAnnouncementProviderService
    {
        private readonly IEventAggregateRepository eventAggregateRepository;

        public AnnouncementProviderService()
        {
            eventAggregateRepository = new EventAggregateRepository();
        }

        public async Task<IEnumerable<Content>> GetContentsAsync(Guid guid)
        {
            var events = await eventAggregateRepository.GetEventsByAnnouncmentID(guid);
            var validAnnouncement =  events.Event.Announcements.FirstOrDefault(a => a.Id == guid);
            
            return validAnnouncement.Content;
        }
    }
}
