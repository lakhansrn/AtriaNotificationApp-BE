using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtriaNotificationApp.API.Models;
using AtriaNotificationApp.BL.Interfaces;
using AtriaNotificationApp.BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtriaNotificationApp.API.Controllers
{
    [Route("api/banner")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IBoardProviderService boardProviderService;

        public BannerController()
        {
            boardProviderService = new BoardProviderService();
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BannerDto>>> Get()
        {
            var boards = await boardProviderService.GetAllValidBoards();

            var bannerDtos = new List<BannerDto>();
            var count = 0;

            foreach (var board in boards)
            {
                foreach (var announcement in board.Announcements.Where(x => x.ShowAsBanner).ToList())
                {
                    bannerDtos.Add(new BannerDto()
                    {
                        ImageUrl = announcement.Img,
                        Title = announcement.Title,
                        ID = count++
                    });
                }
            }

            return bannerDtos;
        }
    }
}