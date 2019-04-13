using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtriaNotificationApp.DAL.Entities;

namespace AtriaNotificationApp.BL.Interfaces
{
    public interface IBoardProviderService
    {
        Task<IEnumerable<Board>> GetAllValidBoards();

        Task<Board> AddBoard(Board board);
        Task<Board> UpdateBoard(Board board);
        Task<IEnumerable<Board>> AddBoards(IEnumerable<Board> boards);
        Task<Board> AddAnnouncement(Guid eventid, Announcement announcement);
        Task<Announcement> UpdateAnnouncement(Guid eventid, Announcement announcement);
        Task<Board> AddContent(Guid event_guid, Guid announcement_guid, Content content);
        Task<Board> UpdateContent(Guid event_guid, Guid announcement_guid, Guid content_id, Content content);
        Task DeleteBoard(Guid guid);
    }
}
