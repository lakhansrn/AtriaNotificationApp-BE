using AtriaNotificationApp.BL.Interfaces;
using AtriaNotificationApp.DAL.Entities;
using AtriaNotificationApp.DAL.Interfaces;
using AtriaNotificationApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.BL.Services
{
    public class BoardProviderService : IBoardProviderService
    {
        private readonly IBoardAggregateRepository boardRepository;

        public BoardProviderService()
        {
            boardRepository = new BoardAggregateRepository();
        }

        public async Task<IEnumerable<Board>> GetAllValidBoards()
        {
            var boardRoots = await boardRepository.GetAllBoardRoots();
            
            return boardRoots.ToList().Select(x=>x.Board);
        }

        public async Task<Board> AddAnnouncement(Guid boardid, Announcement announcement)
        {
            var boardRoots = await boardRepository.AddAnnouncement(boardid, announcement);

            return boardRoots;
        }

        public async Task<Board> UpdateBoard(Board board)
        {
            var boardRoots = await boardRepository.UpdateBoard(board);

            return boardRoots;
        }

        public async Task<Announcement> UpdateAnnouncement(Guid boardid, Announcement announcement)
        {
            var boardRoots = await boardRepository.UpdateAnnouncement(boardid, announcement);

            return boardRoots.Announcements.FirstOrDefault(x => x.Id == announcement.Id);
        }

        public async Task<Board> AddContent(Guid board_guid, Guid announcement_guid, Content content)
        {
            var boardRoots = await boardRepository.AddContent(board_guid, announcement_guid, content);

            return boardRoots;
        }

        public async Task<Board> UpdateContent(Guid board_guid, Guid announcement_guid, Guid content_id, Content content)
        {
            var boardRoots = await boardRepository.UpdateContent(board_guid, announcement_guid, content_id, content);

            return boardRoots;
        }

        public async Task<Board> AddBoard(Board item)
        {

            return await boardRepository.AddBoard(item);
        }

        public async Task<IEnumerable<Board>> AddBoards(IEnumerable<Board> boards)
        {
            return await boardRepository.AddBoards(boards);
        }

        public async Task DeleteBoard(Guid guid)
        {
            await boardRepository.DeleteBoard(guid);
        }
    }
}
