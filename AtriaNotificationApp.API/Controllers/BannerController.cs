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
            var events = await boardProviderService.GetAllValidBoards();

            var bannerDtos = new List<BannerDto>();
            var count = 0;

            foreach (var item in events.Where(x => x.ShowAsBanner).ToList())
            {
                bannerDtos.Add(new BannerDto()
                {
                    ImageUrl = item.BoardBanner,
                    Title = item.BoardName,
                    ID = count++
                });
            }

            return bannerDtos;
        }
    }
}