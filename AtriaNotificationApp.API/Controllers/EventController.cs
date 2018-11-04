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
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventProviderService eventProviderService;
        private readonly IMapper _mapper;
         private readonly IAnnouncementProviderService announcementProviderService;


        public EventController(IMapper mapper)
        {
            eventProviderService = new EventProviderService();
            announcementProviderService = new AnnouncementProviderService();
            _mapper=mapper;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> Get()
        {
            var events = await eventProviderService.GetAllValidEvents();

            List<EventDto> eventDtos = _mapper.Map<List<EventDto>>(events);
      
            return eventDtos;
        }

        [HttpPost]
        public async Task<ActionResult<Event>> Add(EventDto item)
        {
            var selectedEvent = _mapper.Map<Event>(item);

            return await eventProviderService.AddEvent(selectedEvent);
        }

		[HttpPost("{event_guid}/Announcement")]
		public async Task<Event> AddAnnouncement(Guid event_guid, AnnouncementDto announcementDto)
		{
			var announcement = _mapper.Map<Announcement>(announcementDto);
			var event1 = await eventProviderService.AddAnnouncement(event_guid, announcement );
			return event1;
		}

		[HttpPut("{event_guid}/Announcement/{announcement_guid}")]
		public async Task<Event> AddAnnouncement(Guid event_guid, Guid announcement_guid, AnnouncementDto announcementDto)
		{
			var announcement = _mapper.Map<Announcement>(announcementDto);
			var event1 = await eventProviderService.UpdateAnnouncement(event_guid, announcement_guid, announcement);
			return event1;
		}

		[HttpPost("{event_guid}/Announcement/{announcement_guid}/Content")]
        public async Task<Event> AddContent(Guid event_guid, Guid announcement_guid, Content content)
        {
            var event1 = await eventProviderService.AddContent(event_guid, announcement_guid, content);
            return event1;
        }

        [HttpPut("{event_guid}/Announcement/{announcement_guid}/Content/{content_id}")]
        public async Task<Event> AddContent(Guid event_guid, Guid announcement_guid, Guid content_id, Content content)
        {
            var event1 = await eventProviderService.UpdateContent(event_guid, announcement_guid, content_id, content);
            return event1;
        }

        [HttpPost]
        [Route("AddMultiple")]
        public async Task<IEnumerable<Event>> AddMultiple(IEnumerable<EventDto> items)
        {
            var selectedEvent = _mapper.Map<IEnumerable<Event>>(items);

            return await eventProviderService.AddEvents(selectedEvent);
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