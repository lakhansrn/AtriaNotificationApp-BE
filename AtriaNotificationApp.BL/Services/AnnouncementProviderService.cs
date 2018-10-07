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
        private readonly IAnnouncementAggregateRepository announcementAggregateRepository;

        public AnnouncementProviderService()
        {
            announcementAggregateRepository = new AnnouncementAggregateRepository();
        }
        public async Task<IEnumerable<Content>> GetContentsAsync(Guid guid)
        {
            var contents = await announcementAggregateRepository.GetContents(guid);

            return contents.ToList().Select(a => a.Content);
        }
    }
}
