using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtriaNotificationApp.DAL.Entities;

namespace AtriaNotificationApp.DAL.Interfaces
{
    public interface IBoardAggregateRepository
    {
        Task<IEnumerable<BoardAggregateRoot>> GetAllBoardRoots();

        Task<Board> AddBoard(Board board);

        Task<IEnumerable<Board>> AddBoards(IEnumerable<Board> boards);

        Task<Board> AddAnnouncement(Guid boardid, Announcement announcements);

        Task<Board> UpdateBoard(Board board);

        Task<Board> UpdateAnnouncement(Guid boardid, Announcement announcement);

        Task<BoardAggregateRoot> GetBoardsByAnnouncmentID(Guid guid);

        Task<Board> AddContent(Guid board_guid, Guid announcement_guid, Content content);

        Task<Board> UpdateContent(Guid board_guid, Guid announcement_guid, Guid content_id, Content content);

        Task DeleteBoard(Guid boardGuid);
    }
}
