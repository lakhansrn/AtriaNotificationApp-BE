using AtriaNotificationApp.BL.Interfaces;
using AtriaNotificationApp.BL.Models;
using AtriaNotificationApp.DAL.Interfaces;
using AtriaNotificationApp.DAL.Repositories;
using System;
using System.Collections.Generic;
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
            var events = new List<Event>();
            foreach (var item in eventRoots)
            {
                var announcements = new List<Announcement>();

                foreach (var announcement in item.Event.Announcements)
                {
                    announcements.Add(new Announcement()
                    {
                        Description = announcement.Description,
                        Img = announcement.Img,
                        PostedDate = announcement.PostedDate,
                        Title = announcement.Title
                    });
                }

                var @event = new Event()
                {
                    EventName = item.Event.EventName,
                    EventBanner = item.Event.EventBanner,
                    Announcements = announcements,
                    Description = item.Event.Description,
                    ShowAsBanner = item.Event.ShowAsBanner
                };
                events.Add(@event);
            }
            return events;
        }

        public async Task<string> AddEvent(Event item)
        {

            var announcements = new List<AtriaNotificationApp.DAL.Entities.Announcement>();

            foreach (var announcement in item.Announcements)
            {
                announcements.Add(new AtriaNotificationApp.DAL.Entities.Announcement(){
                    Description = announcement.Description,
                        Img = announcement.Img,
                        PostedDate = announcement.PostedDate,
                        Title = announcement.Title
                });
            }    
            var selectedEvent = new AtriaNotificationApp.DAL.Entities.Event()
                {
                    EventName = item.EventName,
                    EventBanner = item.EventBanner,
                    Announcements = announcements,
                    Description = item.Description,
                    ShowAsBanner = item.ShowAsBanner
                };

            return await eventRepository.AddEvent(selectedEvent);
        }
    }
}
