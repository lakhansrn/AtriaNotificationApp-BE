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
        private readonly IEventProviderService eventProviderService;

        public BannerController()
        {
            eventProviderService = new EventProviderService();
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BannerDto>>> Get()
        {
            var events = await eventProviderService.GetAllValidEvents();

            var bannerDtos = new List<BannerDto>();
            var count = 0;

            foreach (var item in events.Where(x => x.ShowAsBanner).ToList())
            {
                bannerDtos.Add(new BannerDto()
                {
                    ImageUrl = item.EventBanner,
                    Title = item.EventName,
                    ID = count++
                });
            }

            return bannerDtos;
        }
    }
}