using AtriaNotificationApp.DAL.Entities;
using AtriaNotificationApp.DAL.Interfaces;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.DAL.Repositories
{
    //TODO- From Lakhan : Improve this class, lot of duplicate and unnecessary code
    public class BoardAggregateRepository : IBoardAggregateRepository
    {
        public async Task<Board> AddBoard(Board board)
        {
            board.InitId();
            board.DateCreatedOn = DateTime.UtcNow;
            board.DateModifiedOn = DateTime.UtcNow;

            board.Announcements.ForEach(x=>{x.InitId();
            x.Content.ForEach(y => y.InitId());
            });

            DocumentDBRepository<Board> boardRepo = new DocumentDBRepository<Board>();
            var result =  await boardRepo.CreateItemAsync(board);
            return result;
        }

        public async Task<IEnumerable<Board>> AddBoards(IEnumerable<Board> boards)
        {
            foreach (var board in boards)
            {
                board.InitId();
                board.DateCreatedOn = DateTime.UtcNow;
                board.DateModifiedOn = DateTime.UtcNow;

                board.Announcements
                .ForEach(x=>{x.InitId();
                             x.Content.ForEach(y => y.InitId());
                            });
            }

            DocumentDBRepository<Board> boardRepo = new DocumentDBRepository<Board>();
            var result =  await boardRepo.CreateItemsAsync(boards);
            return result;
        }

        public async Task<Board> AddAnnouncement(Guid boardid, Announcement announcement)
        {
            DocumentDBRepository<Board> boardRepo = new DocumentDBRepository<Board>();

            Board board1 = await boardRepo.GetItemAsync(boardid);
            announcement.InitId();
            announcement.DateCreatedOn = DateTime.UtcNow;
            announcement.DateModifiedOn = DateTime.UtcNow;

            board1.Announcements.Add(announcement);

            var result = await boardRepo.UpdateItemAsync(boardid, board1);
            return result;
        }

        public async Task<IEnumerable<BoardAggregateRoot>> GetAllBoardRoots()
        {
            DocumentDBRepository<Board> boardRepo = new DocumentDBRepository<Board>();
            ICollection<BoardAggregateRoot> roots = new List<BoardAggregateRoot>();
            try
            {
                var boards = await boardRepo.GetItemsAsync();
                foreach (var item in boards)
                {
                    roots.Add(new BoardAggregateRoot(item));
                }
                return roots;
            }
            catch (Exception m)
            {
                List<BoardAggregateRoot> noRootFound = new List<BoardAggregateRoot>();
                Console.WriteLine(m.Message);
                return noRootFound;
            }
        }

        public async Task<Board> UpdateBoard(Board board)
        {
            DocumentDBRepository<Board> boardRepo = new DocumentDBRepository<Board>();

                Board dbBoard = await boardRepo.GetItemAsync(board.Id);


                dbBoard.BoardName = board.BoardName;
                dbBoard.BoardBanner = board.BoardBanner;
                dbBoard.Description = board.Description;
                dbBoard.ShowAsBanner = board.ShowAsBanner;
                dbBoard.DateModifiedOn = DateTime.UtcNow;
                dbBoard.DateSchedule = board.DateSchedule;
                dbBoard.IsFixed = board.IsFixed;
                dbBoard.Order = board.Order;

                Board updatedBoard = await boardRepo.UpdateItemAsync(dbBoard.Id, dbBoard);

                return updatedBoard;
        }

        public async Task<Board> UpdateAnnouncement(Guid boardid, Announcement announcement)
        {
            DocumentDBRepository<Board> boardRepo = new DocumentDBRepository<Board>();
            IEnumerable<Board> boards = new List<Board>();

            boards = await boardRepo.GetItemsAsync(x => x.Id == boardid);

            Board toBeUpdatedBoard = boards.FirstOrDefault(x => x.Id == boardid);

            toBeUpdatedBoard.DateModifiedOn = DateTime.UtcNow;

            Announcement tobeUpdatedAnnouncement = toBeUpdatedBoard.Announcements.FirstOrDefault(x => x.Id == announcement.Id);
            tobeUpdatedAnnouncement.Title = announcement.Title;
            tobeUpdatedAnnouncement.PostedDate = DateTime.Now;
            tobeUpdatedAnnouncement.Img = announcement.Img;
            tobeUpdatedAnnouncement.Description = announcement.Description;
            tobeUpdatedAnnouncement.DateModifiedOn = DateTime.UtcNow;
            tobeUpdatedAnnouncement.DateSchedule = announcement.DateSchedule;

            toBeUpdatedBoard.Announcements.FirstOrDefault(x => x.Id == announcement.Id).Equals(tobeUpdatedAnnouncement);
            Board updatedBoard = await boardRepo.UpdateItemAsync(toBeUpdatedBoard.Id, toBeUpdatedBoard);

            return updatedBoard;
        }

        public async Task<BoardAggregateRoot> GetBoardsByAnnouncmentID(Guid guid)
        {
            DocumentDBRepository<Board> boardRepo = new DocumentDBRepository<Board>();
            ICollection<BoardAggregateRoot> roots = new List<BoardAggregateRoot>();
            try
            {
                var boards = await boardRepo.GetItemsAsync(x => x.Announcements.Any(y => y.Id == guid));
                return new BoardAggregateRoot(boards.FirstOrDefault());
            }
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
                throw;
            }            
        }

        //TODO: From -> Lakhan : Check if the below implementation is right or not . Is there any unused code??
        public async Task<Board> AddContent(Guid board_guid, Guid announcement_guid, Content content)
        {
            DocumentDBRepository<Board> boardRepo = new DocumentDBRepository<Board>();
            try
            {
                var dbBoards = await boardRepo.GetItemAsync(board_guid); 
                
                List<Announcement> updated_announcement = new List<Announcement>();

                foreach (var announcement in dbBoards.Announcements)
                {
                    if (announcement.Id == announcement_guid)
                    {                       
                        List<Content> updated_content = announcement.Content.Append(new Content()
                                                        { Description = content.Description, Id = Guid.NewGuid(), Image = content.Image,
                                                            IsActive = content.IsActive, IsApproved = content.IsApproved, Posted = DateTime.Now,
                                                            PostedBy = content.PostedBy, Title = content.Title,
                                                            DateCreatedOn=DateTime.UtcNow,
                                                            DateModifiedOn=DateTime.UtcNow}).ToList();    
                        updated_announcement.Add(new Announcement() { Content = updated_content,
                                                                      Description = announcement.Description, Id = announcement.Id, Img = announcement.Img,
                                                                      PostedDate = announcement.PostedDate, Title = announcement.Title,DateCreatedOn=announcement.DateCreatedOn,DateModifiedOn=DateTime.UtcNow });
                    }
                    else
                    {
                        updated_announcement.Add(new Announcement() { Content = announcement.Content, Description = announcement.Description, Id = announcement.Id,
                            Img = announcement.Img, PostedDate = announcement.PostedDate, Title = announcement.Title,
                            DateCreatedOn = announcement.DateCreatedOn,
                            DateModifiedOn = DateTime.UtcNow
                        });
                    }
                }

                dbBoards.Announcements = updated_announcement;

                var res = boardRepo.UpdateItemAsync(board_guid, dbBoards);

                return await res;
            }
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
                throw;
            }
        }

        public async Task<Board> UpdateContent(Guid board_guid, Guid announcement_guid, Guid content_id, Content content)
        {
            DocumentDBRepository<Board> boardRepo = new DocumentDBRepository<Board>();
            content.DateModifiedOn = DateTime.UtcNow;

            try
            {
                var dbBoard = await boardRepo.GetItemAsync(board_guid);

                List<Announcement> updated_announcement = new List<Announcement>();

                foreach (var announcement in dbBoard.Announcements)
                {
                    if (announcement.Id == announcement_guid)
                    {
                        List<Content> updated_content = new List<Content>();
                        foreach (Content dbcontent in announcement.Content)
                        {
                            if (dbcontent.Id == content_id)
                            {
                                updated_content.Add(content);
                            }
                            else
                            {
                                updated_content.Add(dbcontent);
                            }
                        }
                        updated_announcement.Add(new Announcement() { Content = updated_content, Description = announcement.Description, Id = announcement.Id,
                            Img = announcement.Img, PostedDate = announcement.PostedDate, Title = announcement.Title,DateCreatedOn=announcement.DateCreatedOn,DateModifiedOn=DateTime.UtcNow });
                    }
                    else
                    {
                        updated_announcement.Add(new Announcement() { Content = announcement.Content,
                            Description = announcement.Description, Id = announcement.Id, Img = announcement.Img,
                            PostedDate = announcement.PostedDate, Title = announcement.Title,
                            DateCreatedOn = announcement.DateCreatedOn,
                            DateModifiedOn = DateTime.UtcNow
                        });
                    }
                }

                dbBoard.Announcements = updated_announcement;

                var res = boardRepo.UpdateItemAsync(board_guid, dbBoard);

                return await res;
            }
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
                throw;
            }
        }

        public async Task DeleteBoard(Guid boardGuid)
        {
            DocumentDBRepository<Board> boardRepo = new DocumentDBRepository<Board>();

            await boardRepo.DeleteItemAsync(boardGuid);
        }
    }
}