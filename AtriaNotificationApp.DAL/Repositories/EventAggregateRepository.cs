using AtriaNotificationApp.DAL.Entities;
using AtriaNotificationApp.DAL.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.DAL.Repositories
{
    public class EventAggregateRepository : IEventAggregateRepository
    {
        public async Task<string> AddEvent(Event @event)
        {
            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            var document  = await eventRepo.CreateItemAsync(@event);
            return JsonConvert.SerializeObject(document);
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
    }
}