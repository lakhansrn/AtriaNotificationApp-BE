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
    public class EventProviderService : IEventProviderService
    {
        private readonly IEventAggregateRepository eventRepository;

        public EventProviderService()
        {
            eventRepository = new EventAggregateRepository();
        }

        public async Task<IEnumerable<Event>> GetAllValidEvents()
        {
            var eventRoots = await eventRepository.GetAllEventRoots();
            
            return eventRoots.ToList().Select(x=>x.Event);
        }

        public async Task<Event> AddContent(Guid event_guid, Guid announcement_guid, Content content)
        {
            var eventRoots = await eventRepository.AddContent(event_guid, announcement_guid, content);

            return eventRoots;
        }

        public async Task<Event> UpdateContent(Guid event_guid, Guid announcement_guid, Guid content_id, Content content)
        {
            var eventRoots = await eventRepository.UpdateContent(event_guid, announcement_guid, content_id, content);

            return eventRoots;
        }

        public async Task<Event> AddEvent(Event item)
        {

            return await eventRepository.AddEvent(item);
        }

        public async Task<IEnumerable<Event>> AddEvents(IEnumerable<Event> events)
        {
            return await eventRepository.AddEvents(events);
        }

		public async Task<Event> AddAnnouncement(Guid guid, Announcement announcement)
		{
			var eventRoots = await eventRepository.AddAnnouncement(guid, announcement);

			return eventRoots;
		}

		public Task<Event> UpdateAnnouncement(Guid guid, Guid announcment_guid, Announcement announcement)
		{
			throw new NotImplementedException();
		}
	}
}
