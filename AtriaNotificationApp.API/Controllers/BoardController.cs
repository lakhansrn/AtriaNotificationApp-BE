using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtriaNotificationApp.API.Models;
using AtriaNotificationApp.BL.Interfaces;
using AtriaNotificationApp.BL.Services;
using AtriaNotificationApp.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtriaNotificationApp.API.Controllers
{
    [Route("api/board")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IBoardProviderService boardProviderService;
        private readonly IMapper _mapper;
         private readonly IAnnouncementProviderService announcementProviderService;


        public BoardController(IMapper mapper)
        {
            boardProviderService = new BoardProviderService();
            announcementProviderService = new AnnouncementProviderService();
            _mapper=mapper;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardDto>>> Get()
        {
            var boards = await boardProviderService.GetAllValidBoards();

            List<BoardDto> boardDtos = _mapper.Map<List<BoardDto>>(boards);
      
            return boardDtos;
        }

        [HttpGet("FixedBoards")]
        public async Task<ActionResult<IEnumerable<BoardDto>>> GetFixedBoards()
        {
            var boards = await boardProviderService.GetAllValidBoards();

            List<BoardDto> boardDtos = _mapper.Map<List<BoardDto>>(boards);

            List<BoardDto> orderedBoards =  boardDtos.Where(board => board.IsFixed).OrderBy(x => x.Order).ToList();

            return orderedBoards;
        }

        // Delete api/values
        [HttpDelete]
        public async Task Delete(Guid guid)
        {
            await boardProviderService.DeleteBoard(guid);
        }

        [HttpPost]
        public async Task<ActionResult<Board>> Add(BoardDto item)
        {
            var selectedBoard = _mapper.Map<Board>(item);

            return await boardProviderService.AddBoard(selectedBoard);
        }

        [HttpPost("{boardid}/Announcement")]
        public async Task<ActionResult<Board>> AddAnnouncement(Guid boardid, Announcement announcement)
        {
            var selectedBoard = _mapper.Map<Announcement>(announcement);

            return await boardProviderService.AddAnnouncement(boardid, announcement);
        }

        [HttpPut]
        public async Task<ActionResult<Board>> UpdateBoard(BoardDto item)
        {
            var selectedBoard = _mapper.Map<Board>(item);

            return await boardProviderService.UpdateBoard(selectedBoard);
        }

        [HttpPut("{boardid}/Announcement")]
        public async Task<ActionResult<Announcement>> UpdateAnnouncement(Guid boardid, Announcement announcement)
        {

            return await boardProviderService.UpdateAnnouncement(boardid, announcement);
        }

        [HttpPost("{board_guid}/Announcement/{announcement_guid}/Content")]
        public async Task<Board> AddContent(Guid board_guid, Guid announcement_guid, Content content)
        {
            var board1 = await boardProviderService.AddContent(board_guid, announcement_guid, content);
            return board1;
        }

        [HttpPut("{board_guid}/Announcement/{announcement_guid}/Content/{content_id}")]
        public async Task<Board> AddContent(Guid board_guid, Guid announcement_guid, Guid content_id, Content content)
        {
            var board1 = await boardProviderService.UpdateContent(board_guid, announcement_guid, content_id, content);
            return board1;
        }

        [HttpPost]
        [Route("AddMultiple")]
        public async Task<IEnumerable<Board>> AddMultiple(IEnumerable<BoardDto> items)
        {
            var selectedBoard = _mapper.Map<IEnumerable<Board>>(items);

            return await boardProviderService.AddBoards(selectedBoard);
        }

        [HttpGet("Announcement/{guid}/Content")]
        public async Task<ActionResult<List<ContentDto>>> GetContentsAsync(Guid guid)
        {
            var contents = await announcementProviderService.GetContentsAsync(guid);
            List<ContentDto> contentDtos = _mapper.Map<List<ContentDto>>(contents);
            return contentDtos;
        }

    }
}