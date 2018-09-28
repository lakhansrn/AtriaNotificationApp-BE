using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtriaNotificationApp.API.Models;
using AtriaNotificationApp.BL.Interfaces;
using AtriaNotificationApp.BL.Models;
using AtriaNotificationApp.BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtriaNotificationApp.API.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventProviderService eventProviderService;

        public EventController()
        {
            eventProviderService = new EventProviderService();
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> Get()
        {
            var events = await eventProviderService.GetAllValidEvents();

            var eventDtos = new List<EventDto>();
            foreach (var item in events)
            {
                var announcements = new List<AnnouncementDto>();

                foreach (var announcement in item.Announcements)
                {
                    announcements.Add(new AnnouncementDto()
                    {
                        Description = announcement.Description,
                        Img = announcement.Img,
                        PostedDate = announcement.PostedDate,
                        Title = announcement.Title
                    });
                }

                eventDtos.Add(new EventDto()
                {
                    EventName = item.EventName,
                    Description = item.Description,
                    EventBanner = item.EventBanner,
                    ShowAsBanner = item.ShowAsBanner,
                    Announcements = announcements
                });
            }
            return eventDtos;
        }
    }
}