using AtriaNotificationApp.DAL.Entities;
using AtriaNotificationApp.DAL.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.DAL.Repositories
{
    public class EventAggregateRepository : IEventAggregateRepository
    {
        public async Task<Event> AddEvent(Event @event)
        {
            @event.InitId();
            @event.Announcements.ForEach(x=>{x.InitId();
            x.Content.ForEach(y => y.InitId());
            });

            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            var result =  await eventRepo.CreateItemAsync(@event);
            return result;
        }

        public async Task<IEnumerable<Event>> AddEvents(IEnumerable<Event> events)
        {
            foreach (var @event in events)
            {
                @event.InitId();
                @event.Announcements
                .ForEach(x=>{x.InitId();
                             x.Content.ForEach(y => y.InitId());
                            });
            }

            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            var result =  await eventRepo.CreateItemsAsync(events);
            return result;
        }

        public async Task<IEnumerable<EventAggregateRoot>> GetAllEventRoots()
        {
            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            ICollection<EventAggregateRoot> roots = new List<EventAggregateRoot>();
            try
            {
                var events = await eventRepo.GetItemsAsync();
                foreach (var item in events)
                {
                    roots.Add(new EventAggregateRoot(item));
                }
                return roots;
            }
            catch (Exception m)
            {
                List<EventAggregateRoot> noRootFound = new List<EventAggregateRoot>();
                Console.WriteLine(m.Message);
                return noRootFound;
            }
        }

        public async Task<EventAggregateRoot> GetEventsByAnnouncmentID(Guid guid)
        {
            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            ICollection<EventAggregateRoot> roots = new List<EventAggregateRoot>();
            try
            {
                var events = await eventRepo.GetItemsAsync(x => x.Announcements.Any(y => y.Id == guid));
                return new EventAggregateRoot(events.FirstOrDefault());
            }
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
                return null;
            }            
        }
    }
}