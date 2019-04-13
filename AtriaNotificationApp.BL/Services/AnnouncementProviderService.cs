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
        private readonly IBoardAggregateRepository boardAggregateRepository;

        public AnnouncementProviderService()
        {
            boardAggregateRepository = new BoardAggregateRepository();
        }

        public async Task<IEnumerable<Content>> GetContentsAsync(Guid guid)
        {
            var boards = await boardAggregateRepository.GetBoardsByAnnouncmentID(guid);
            var validAnnouncement =  boards.Board.Announcements.FirstOrDefault(a => a.Id == guid);
            
            return validAnnouncement.Content;
        }
    }
}
