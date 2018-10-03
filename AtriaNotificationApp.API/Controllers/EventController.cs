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
                    var contents = new List<ContentDto>();

                    foreach (var content in announcement.Content)
                    {
                        var announcer = new AnnouncerDto()
                        {
                            Name = content.PostedBy.Name,
                            Department = content.PostedBy.Department,
                            Email = content.PostedBy.Email,
                            Pno = content.PostedBy.Pno
                        };
                        contents.Add(new ContentDto()
                        {
                            Title = content.Title,
                            Posted = content.Posted,
                            Image = content.Image,
                            PostedBy = announcer,
                            Description = content.Description,
                        });
                    }
                    announcements.Add(new AnnouncementDto()
                    {
                        Description = announcement.Description,
                        Img = announcement.Img,
                        PostedDate = announcement.PostedDate,
                        Title = announcement.Title,
                        Content = contents
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

        [HttpPost]
        public async Task<ActionResult<string>> Add(EventDto item)
        {
            var announcements = new List<AtriaNotificationApp.BL.Models.Announcement>();

            foreach (var announcement in item.Announcements)
            {
                var contents = new List<AtriaNotificationApp.BL.Models.Content>();

                foreach (var content in announcement.Content)
                {
                    var announcer = new AtriaNotificationApp.BL.Models.Announcer() {
                        Name = content.PostedBy.Name,
                        Department = content.PostedBy.Department,
                        Email = content.PostedBy.Email,
                        Pno = content.PostedBy.Pno
                    };
                    contents.Add(new AtriaNotificationApp.BL.Models.Content() {
                        Title = content.Title,
                        Posted = content.Posted,
                        Image = content.Image,
                        PostedBy = announcer,
                        Description = content.Description,
                    });
                }
                announcements.Add(new AtriaNotificationApp.BL.Models.Announcement(){
                    Description = announcement.Description,
                    Img = announcement.Img,
                    PostedDate = announcement.PostedDate,
                    Title = announcement.Title,
                    Content = contents
                });
            }    
            var selectedEvent = new AtriaNotificationApp.BL.Models.Event()
            {
                EventName = item.EventName,
                EventBanner = item.EventBanner,
                Announcements = announcements,
                Description = item.Description,
                ShowAsBanner = item.ShowAsBanner
            };

            return await eventProviderService.AddEvent(selectedEvent);
        }

    }
}