using AtriaNotificationApp.DAL.Entities;
using AtriaNotificationApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.DAL.Repositories
{
    public class EventAggregateRepository : IEventAggregateRepository
    {
        public async Task<IEnumerable<EventAggregateRoot>> GetAllEventRoots()
        {
            DocumentDBRepository<Event> Event = new DocumentDBRepository<Event>();
            ICollection<EventAggregateRoot> roots = new List<EventAggregateRoot>();
            try
            {
                var events = await Event.GetItemsAsync();
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